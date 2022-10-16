<template>
    <div class="bookImport">
        <h2>書籍匯入</h2>
        <div class="filter rounded border px-2 py-3 bg-light">
            <div class="row">
                <div class="col-auto">
                    <!--檔案上傳-->
                    <ValidationProvider v-slot="{ valid, errors, validate}" name="檔案" rules="ext:xlsx,xls">
                        <div class="input-group">
                            <input id="file" :class="[{'is-invalid': valid===false}, 'form-control']"
                                   name="file" type="file" @change="validate">
                            <button class="btn btn-primary" type="button" @click="uploadExcel">上傳</button>
                            <span class="invalid-feedback">{{ errors[0] }}</span>
                        </div>
                    </ValidationProvider>
                </div>
                <div class="col-12 mt-3">
                    <template v-if="bookList.length>0">
                        <button class="btn btn-primary" @click="save">批次儲存</button>
                        <button class="btn btn-primary" @click="clear">清除(資料)</button>
                    </template>
                    <!-- 錯誤解析(Button) -->
                    <button v-if="isfailLog" type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#exampleModal">
                        錯誤解析
                    </button>
                    <!-- 互動視窗(Modal) -->
                    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">錯誤解析</h5>
                                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                </div>
                                <div class="modal-body">
                                    {{errorList}}
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">關閉</button>
                                    <!--<button type="button" class="btn btn-primary">Save changes</button>-->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--匯入資料-->
        <div class="table-responsive">
            <table class="table table-bordered table-hover">
                <colgroup>
                    <col style="min-width: 3em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 6em;">
                    <col style="min-width: 6em;">
                    <col style="min-width: 9em;">
                    <col style="min-width: 9em;">
                    <col style="min-width: 9em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                    <col style="min-width: 14em;">
                </colgroup>
                <thead>
                    <tr>
                        <th>#</th>
                        <th>書名</th>
                        <th>狀態</th>
                        <th>庫存數量</th>
                        <th>ISBN</th>
                        <th>ISSN</th>
                        <th>GPN(必填)</th>
                        <th>出版單位(必填)</th>
                        <th>出版情況(必填)</th>
                        <th>作者資訊(必填)</th>
                        <th>出版日期(必填)</th>
                        <th>版次</th>
                        <th>書封連結</th>
                        <th>書籍分類</th>
                        <th>出版品分類(必填)</th>
                        <th>主題分類</th>
                        <th>施政分類</th>
                        <th>級別</th>
                        <th>頁數(必填)</th>
                        <th>開數</th>
                        <th>裝訂</th>
                        <th>語言(必填)</th>
                        <th>書籍介紹</th>
                        <th>目次</th>
                        <th>價格(必填)</th>
                        <th>適用對象</th>
                        <th>資料類型</th>
                        <th>附件(必填)</th>
                        <th>出版品網址-線上版或試閱版</th>
                        <th>播放時間長度(必填)</th>
                        <th>字號</th>
                        <th>權利範圍</th>
                        <th>停刊註記</th>
                        <th>授權資訊(必填)</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in bookList">
                        <td>{{index + 1}}</td>
                        <td>{{item.title}}</td>
                        <td>{{item.status}}</td>
                        <td>{{item.numberOfCopies}}</td>
                        <td>{{item.isbn}}</td>
                        <td>{{item.issn}}</td>
                        <td>{{item.gpn}}</td>
                        <td>{{item.publisher}}</td>
                        <td>{{item.rightCondition}}</td>
                        <td>{{item.creator}}</td>
                        <td>{{item.publishDate | momentTW}}</td>
                        <td>{{item.edition}}</td>
                        <td>{{item.cover}}</td>
                        <td>{{item.classify}}</td>
                        <td>{{item.gpntype}}</td>
                        <td>{{item.subject}}</td>
                        <td>{{item.governance}}</td>
                        <td>{{item.grade}}</td>
                        <td>{{item.pages}}</td>
                        <td>{{item.size}}</td>
                        <td>{{item.binding}}</td>
                        <td>{{item.language}}</td>
                        <td>{{item.introduction}}</td>
                        <td>{{item.catalog}}</td>
                        <td>{{item.price}}</td>
                        <td>{{item.targetPeople}}</td>
                        <td>{{item.types}}</td>
                        <td>{{item.attachment}}</td>
                        <td>{{item.url}}</td>
                        <td>{{item.duration}}</td>
                        <td>{{item.numbers}}</td>
                        <td>{{item.restriction}}</td>
                        <td>{{item.ceasedDate}}</td>
                        <td>{{item.authority}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</template>
<script>
    import mixin from "../../mixin.js";
    import modal from "bootstrap/js/dist/modal"

    export default {
        name: "BookImport",
        mixins: [mixin],
        data() {
            return {
                bookList: [],
                isfailLog: false,
                errorList: {},
            }
        },
        methods: {
            //清除上傳的列表資料
            clear() {
                this.bookList = [];
                this.errorList = {}; //重置錯誤log
                this.isfailLog = false;
            },
            //批次儲存
            save() {
                this.$axios.post("https://localhost:44323/api/Book/PostBooks", {
                    books: this.bookList
                })
                    .then((response) => {
                        console.log(response);
                        if (response.status === 200) {
                            if (response.data.success === true) {
                                alert("新增成功");

                            }
                            else {
                                alert("新增失敗");
                            }
                        }
                    })
                    .catch((error) => {
                        console.log("error", error.response.data.errors);
                        //this.$vm.errorList
                        this.errorList = error.response.data.errors;
                        this.isfailLog = true;
                    })
            },
            getBookList() {

            },
            //上傳Excel
            uploadExcel() {
                const formData = new FormData();
                let excelFile = document.getElementById('file');
                formData.append('file', excelFile.files[0]);

                this.$axios.post("https://localhost:44323/api/Book/ImportExcel",
                    formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data;',
                    }
                })
                    .then((response) => {
                        if (response.status === 200) {
                            alert("匯入成功");
                            document.getElementById('file').value = "";

                            this.bookList = response.data.data;
                            this.errorList = {}; //重置錯誤log
                            this.isfailLog = false;
                        }
                        else {
                            alert("匯入失敗");
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
        },
        created() {
        }
    };
</script>

<style>
</style>