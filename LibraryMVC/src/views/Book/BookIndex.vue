<template>
    <div class="bookIndex">
        <h2>書籍</h2>
        <div class="filter rounded border px-2 py-3 bg-light">
            <div class="row">
                <div class="col-auto">
                    書名：<input type="text" v-model="title" class="form-control" />
                </div>
                <div class="col-auto">
                    ISBN：<input type="text" v-model="isbn" class="form-control" />
                </div>
                <div class="col-12 mt-3">
                    <button class="btn btn-primary" @click="search">搜尋</button>
                    <button class="btn btn-primary" @click="addBook">新增</button>
                    <button class="btn btn-primary" href="/Book/WebSample1" >匯出</button>
                </div>
            </div>
        </div>
        
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
                <col style="width: 20em;">
                <col style="width: 8em;">
                <col style="width: 14em;">
                <col style="width: 5em;">
                <col style="width: 5em;">
            </colgroup>
            <thead>
                <tr>
                    <th>#</th>
                    <th>書名</th>
                    <th>ISBN</th>
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
                    <td>{{item.publisher}}</td>
                    <td>{{item.publishDate}}</td>
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
</template>
<script>
    import mixin from "../../mixin.js";

    export default {
        name: "BookIndex",
        mixins: [mixin],
        data() {
            return {
                title: '',
                isbn: '',
                bookList: [],
            }
        },
        methods: {
            getBookList() {
                let filter = {
                    title: this.title,
                    isbn: this.isbn,
                    PaginationFilter: {
                        PageNumber: this.pageNumber,
                        PageSize: this.pageSize
                    }
                };
                this.$axios.post("https://localhost:44323/api/Book/List",
                    filter)
                    .then((response) => {

                        this.bookList = response.data.data;
                        //總頁數
                        this.totalPages = response.data.totalPages;
                        //總筆數
                        this.totalRecords = response.data.totalRecords;
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            deleteBook(id) {
                this.$axios.delete(`https://localhost:44323/api/Book/${id}`)
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
            exportExcel() {

            }
        },
        created() {
            this.getBookList();
        }

    };
</script>

<style>
</style>