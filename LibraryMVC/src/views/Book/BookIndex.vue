<template>
    <div class="container-fluid">
        <h2>圖書</h2>
        <form @submit.prevent="search" class="row">
            <div class="col-auto">
                書名：<input type="text" v-model="title" class="form-control" />
            </div>
            <div class="col-auto">
                ISBN：<input type="text" v-model="isbn" class="form-control" />
            </div>
            <div class="col-12 mt-2">
                <button type="submit" class="btn btn-primary">搜尋</button>
            </div>
        </form>
        <paginate v-model="pageNumber"
                  :page-count="totalPages"
                  :click-handler="getBookList"
                  :prev-text="'上一頁'"
                  :next-text="'下一頁'"
                  :container-class="'pagination'"
                  :page-class="'page-item'">
        </paginate>

        總筆數{{totalRecords}}

        <table class="table table-bordered table-hover">
            <thead>
                <tr>
                    <th>#</th>
                    <th>title</th>
                    <th>isbn</th>
                    <th>publisher</th>
                    <th>publishDate</th>
                    <th></th>
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
            search() {
                //搜尋後，從第一頁開始
                this.pageNumber = 1;
                this.getBookList();
            }
        },
        created() {
            this.getBookList();
        }

    };
</script>

<style>
</style>