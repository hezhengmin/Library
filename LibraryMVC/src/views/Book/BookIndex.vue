<template>
    <div class="bookIndex">
        <loading loader="spinner"
                 :active.sync="isLoading"
                 :can-cancel="true"
                 :is-full-page="false">
        </loading>

        <div class="card">
            <div class="card-header">
                <h2 class="card-title">書籍列表</h2>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-auto">
                        書名：<input type="text" v-model="title" class="form-control" />
                    </div>
                    <div class="col-auto">
                        ISBN：<input type="text" v-model="isbn" class="form-control" />
                    </div>
                    <div class="col-12 mt-3">
                        <button class="btn btn-primary btn-fill" @click="search">搜尋</button>
                        <button class="btn btn-primary btn-fill" @click="addBook">新增</button>
                        <button class="btn btn-success btn-fill" @click="exportExcel">匯出</button>
                        <button class="btn btn-warning btn-fill" @click="importExcel">匯入</button>
                    </div>
                </div>
            </div>
        </div>
        <!--資料列-->
        <div class="card mt-3">
            <div class="card-body">
                <div class="d-flex justify-content-between align-items-center">
                    <div class="p-2">
                        第 {{pageNumber}} 頁，總共 {{totalRecords}} 筆
                    </div>
                    <div class="p-2">
                        <paginate v-model="pageNumber"
                                  :page-count="totalPages"
                                  :click-handler="getBookList"
                                  :prev-text="'上一頁'"
                                  :next-text="'下一頁'"
                                  :container-class="'pagination'"
                                  :page-class="'page-item'"
                                  :page-link-class="'page-link'"
                                  :prev-link-class="'page-link'"
                                  :next-link-class="'page-link'">
                        </paginate>
                    </div>
                </div>
                <table class="table table-bordered table-hover">
                    <colgroup>
                        <col style="width: 3em;">
                        <col style="width: 18em;">
                        <col style="width: 8em;">
                        <col style="width: 3em;">
                        <col style="width: 14em;">
                        <col style="width: 7em;">
                        <col style="width: 5em;">
                    </colgroup>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>書名</th>
                            <th>ISBN</th>
                            <th>數量</th>
                            <th>出版單位</th>
                            <th>出版日期</th>
                            <th>功能</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in bookList" :key="item.id">
                            <td>{{index + 1}}</td>
                            <td>{{item.title}}</td>
                            <td>{{item.isbn}}</td>
                            <td>{{item.numberOfCopies}}</td>
                            <td>{{item.publisher}}</td>
                            <td>{{item.publishDate | momentTW}}</td>
                            <td>
                                <router-link :to="{
                                 name: 'BookEdit',
                                 params: { id: item.id }
                                 }">
                                    編輯
                                </router-link>
                                <a href="" @click.prevent="deleteBook(item.id)">刪除</a>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>
<script>
    import mixin from "../../mixin.js";
    import { apiPostBookList, apiDeleteBook, apiPostBookExportExcel } from "api";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/vue-loading.css';

    export default {
        name: "BookIndex",
        mixins: [mixin],
        data() {
            return {
                title: '',
                isbn: '',
                bookList: [],
                isLoading: true, //載入
            }
        },
        components: {
            Loading
        },
        methods: {
            getBookList() {
                let filter = {
                    title: this.title,
                    isbn: this.isbn,
                    PaginationResult: {
                        PageNumber: this.pageNumber,
                        PageSize: this.pageSize
                    }
                };
                apiPostBookList(filter)
                    .then((response) => {

                        this.bookList = response.data.data;
                        //總頁數
                        this.totalPages = response.data.totalPages;
                        //總筆數
                        this.totalRecords = response.data.totalRecords;

                        //資料Loading載入結束
                        this.isLoading = false
                    })
                    .catch((error) => {
                        console.log(`apiBookList`, error);
                    })
            },
            deleteBook(id) {
                if (!confirm("確定要刪除?"))
                    return;
                apiDeleteBook(`/Book/${id}`)
                    .then((response) => {
                        if (response.status === 204) {
                            alert("刪除成功");
                            this.getBookList();
                        }
                        else {
                            alert("刪除失敗");
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            search() {
                //搜尋後，從第一頁開始
                this.pageNumber = 1;
                this.getBookList();
            },
            //新增書籍
            addBook() {
                //空guid代表新增
                this.$router.push({ name: 'BookEdit', params: { id: '00000000-0000-0000-0000-000000000000' } })
            },
            //匯出
            exportExcel() {
                const method = 'POST';
                const url = "/Book/ExportExcel";
                let filter = {
                    title: this.title,
                    isbn: this.isbn,
                    PaginationResult: {
                        PageNumber: this.pageNumber,
                        PageSize: this.pageSize
                    }
                };

                apiPostBookExportExcel({
                    url,
                    method,
                    data: filter,
                    responseType: 'blob', //important
                })
                    .then(({ data }) => {
                        const downloadUrl = window.URL.createObjectURL(new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }));
                        const link = document.createElement('a');
                        link.href = downloadUrl;
                        link.setAttribute('download', "匯出書籍列表"); //any other extension
                        document.body.appendChild(link);
                        link.click();
                        link.remove();
                    })
                    .catch(err => {
                        console.log(err);
                        reject(err);
                    })
            },
            //匯入
            importExcel() {
                this.$router.push({ name: 'BookImport' })
            }
        },
        created() {
            this.getBookList();
        }

    };
</script>

<style>
</style>