<template>
    <div class="LoanIndex">
        <h2>書籍借閱</h2>
        <div class="filter rounded border px-2 py-3 bg-light">
            <div class="row">
                <div class="col-auto">
                    書名：<input type="text" v-model="BookTitle" class="form-control" />
                </div>
                <div class="col-auto">
                    帳號：<input type="text" v-model="UserId" class="form-control" />
                </div>
                <div class="col-12 mt-3">
                    <button class="btn btn-primary" @click="search">搜尋</button>
                    <button class="btn btn-primary" @click="addLoan">新增</button>
                    <button class="btn btn-primary" @click="exportExcel">匯出</button>
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
                          :click-handler="getLoanList"
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
                <col style="width: 7em;">
                <col style="width: 7em;">
                <col style="width: 7em;">
                <col style="width: 5em;">
            </colgroup>
            <thead>
                <tr>
                    <th>#</th>
                    <th>書名</th>
                    <th>帳號</th>
                    <th>借出開始日期</th>
                    <th>借出結束日期</th>
                    <th>書籍歸還日期</th>
                    <th>功能</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(item, index) in LoanList" :key="item.id">
                    <td>{{index + 1}}</td>
                    <td>{{item.bookTitle}}</td>
                    <td>{{item.userId}}</td>
                    <td>{{item.issueDate | momentTW}} </td>
                    <td>{{item.dueDate | momentTW }}</td>
                    <td>{{item.returnDate | momentTW }}</td>
                    <td>
                        <router-link :to="{
                                 name: 'LoanEdit',
                                 params: { id: item.id }
                                 }">
                            編輯
                        </router-link>
                        <a href="" @click.prevent="deleteLoan(item.id)">刪除</a>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>
<script>
    import mixin from "../../mixin.js";
    import { apiLoanList } from "api";

    export default {
        name: "LoanIndex",
        mixins: [mixin],
        data() {
            return {
                BookTitle: '',
                UserId: '',
                LoanList: [],
            }
        },
        methods: {
            getLoanList() {
                let filter = {
                    BookTitle: this.BookTitle,
                    UserId: this.UserId,
                    PaginationResult: {
                        PageNumber: this.pageNumber,
                        PageSize: this.pageSize
                    }
                };
                apiLoanList(filter)
                    .then((response) => {

                        this.LoanList = response.data.data;
                        //總頁數
                        this.totalPages = response.data.totalPages;
                        //總筆數
                        this.totalRecords = response.data.totalRecords;
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            deleteLoan(id) {
                this.$axios.delete(`https://localhost:44323/api/Loan/${id}`)
                    .then((response) => {
                        if (response.status === 204) {
                            alert("刪除成功");
                            this.getLoanList();
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
                this.getLoanList();
            },
            addLoan() {
                //空guid代表新增
                this.$router.push({ name: 'LoanEdit', params: { id: '00000000-0000-0000-0000-000000000000' } })
            },
            //檔案下載
            exportExcel() {
                const method = 'POST';
                const url = `https://localhost:44323/api/Loan/Export`;
                let filter = {
                    BookTitle: this.BookTitle,
                    UserId: this.UserId
                };

                this.$axios.request({
                    url,
                    method,
                    data: filter,
                    responseType: 'blob', //important
                })
                    .then(({ data }) => {
                        const downloadUrl = window.URL.createObjectURL(new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' }));
                        const link = document.createElement('a');
                        link.href = downloadUrl;
                        link.setAttribute('download', "匯出書籍借閱紀錄"); //any other extension
                        document.body.appendChild(link);
                        link.click();
                        link.remove();
                    })
                    .catch(err => {
                        console.log(err);
                        reject(err);
                    })
            },
        },
        created() {
            //書籍借閱列表
            this.getLoanList();
        }
    };
</script>

<style>
</style>