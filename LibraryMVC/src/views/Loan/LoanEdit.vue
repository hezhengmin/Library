<template>
    <div class="loanEdit">
        <ValidationObserver ref="observer" v-slot="{ invalid }" tag="form">
            <div class="card">
                <div class="card-header">
                    <div class="d-flex justify-content-between">
                        <div class="py-2">
                            <h2 class="card-title">{{title}}</h2>
                        </div>
                        <div class="py-2">
                            <button class="btn btn-primary btn-fill " :disabled="invalid" type="button" @click="onSubmit">確認</button>
                            <button type="button" @click="$router.go(-1)" class="btn btn-primary btn-fill ">
                                回上一頁
                            </button>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                            <h4>借閱資訊</h4>
                            <div class="pb-2">
                                <span class="text-danger">*</span>
                                <label for="accountId" class="form-label">借閱帳號</label>
                                <span class="text-primary">{{loan.userId}}</span>

                                <ValidationProvider v-slot="{ valid, errors }"
                                                    name="借閱帳號" rules="excluded:00000000-0000-0000-0000-000000000000">
                                    <Select2 v-model="loan.accountId"
                                             :settings="{ width: '300px' }"
                                             :options="accountSelectList"
                                             @change="accountChangeEvent($event)"
                                             @select="accountSelectEvent($event)" />
                                    <span class="text-danger">{{ errors[0] }}</span>
                                </ValidationProvider>
                            </div>
                            <div class="pb-2">
                                <span class="text-danger">*</span>
                                <label for="bookTitle" class="form-label">借閱書名</label>
                                <span class="text-primary">{{loan.bookTitle}}</span>
                                <ValidationProvider v-slot="{ valid, errors }"
                                                    name="借閱書名" rules="excluded:00000000-0000-0000-0000-000000000000">
                                    <Select2 v-model="loan.bookId"
                                             :settings="{ width: '300px' }"
                                             :options="bookSelectList"
                                             @change="bookChangeEvent($event)"
                                             @select="bookSelectEvent($event)" />
                                    <span class="text-danger">{{ errors[0] }}</span>
                                </ValidationProvider>
                            </div>
                            <div class="pb-2">
                                <span class="text-danger">*</span>
                                <label for="issueDate" class="form-label">借閱開始日期</label>
                                <ValidationProvider v-slot="{ valid, errors }"
                                                    name="借閱開始日期" rules="required">
                                    <date-picker ref="issueDate"
                                                 v-model="loan.issueDate"
                                                 value-type="format"
                                                 format="YYYY-MM-DD"></date-picker>
                                    <span class="text-danger">{{ errors[0] }}</span>
                                </ValidationProvider>

                            </div>
                            <div class="pb-2">
                                <span class="text-danger">*</span>
                                <label for="dueDate" class="form-label">借閱結束日期</label>
                                <ValidationProvider v-slot="{ valid, errors }"
                                                    name="借閱結束日期" rules="required">
                                    <date-picker ref="dueDate"
                                                 v-model="loan.dueDate"
                                                 value-type="format"
                                                 format="YYYY-MM-DD"></date-picker>
                                    <span class="text-danger">{{ errors[0] }}</span>
                                </ValidationProvider>
                            </div>
                            <div class="pb-2">
                                <label for="returnDate" class="form-label">借閱歸還日期</label>
                                <date-picker id="returnDate"
                                             v-model="loan.returnDate"
                                             value-type="format"
                                             format="YYYY-MM-DD"></date-picker>
                            </div>
                        </div>
                    </div>
                    <div class="d-flex my-2">
                        <button class="btn btn-primary btn-fill me-2" :disabled="invalid" type="button" @click="onSubmit">確認</button>
                        <button type="button" @click="$router.go(-1)" class="btn btn-primary btn-fill ">
                            回上一頁
                        </button>
                    </div>
                </div>
            </div>
        </ValidationObserver>
    </div>
</template>
<script>
    import { apiGetAccountSelectList, apiPostLoan, apiGetBookSelectList, apiGetLoan, apiPutLoan } from 'api'
    export default {
        name: "LoanEdit",
        data() {
            return {
                loan: {
                    accountId: "00000000-0000-0000-0000-000000000000",
                    bookId: "00000000-0000-0000-0000-000000000000",
                    bookTitle: "",
                    id: "",
                    startEndDate: [],
                    issueDate: "",
                    dueDate: "",
                    returnDate: null,
                    userId: "",
                },
                bookSelectList: [
                ],
                accountSelectList: [
                ],
            }
        },
        computed: {
            //是否為編輯
            isEdit() {
                return this.$route.params.id !== "00000000-0000-0000-0000-000000000000";
            },
            title() {
                return this.isEdit ? "借閱編輯" : "借閱新增";
            }
        },
        watch: {

        },
        methods: {
            bookChangeEvent(val) {
                console.log(val);
            },
            bookSelectEvent({ id, text }) {
                console.log({ id, text })

                this.loan.bookTitle = text;
            },
            accountChangeEvent(val) {
                console.log(val);
            },
            accountSelectEvent({ id, text }) {
                console.log({ id, text })
                this.loan.userId = text;
            },
            //提交前驗證
            async onSubmit() {
                const isValid = await this.$refs.observer.validate();

                if (isValid) {
                    if (this.isEdit) {
                        this.updateLoan();
                    }
                    else { //新增
                        this.addLoan();
                    }
                }
                else {
                    console.log(`isValid ${isValid}`);
                }
            },
            //帳號列表清單
            initAccountSelectList() {
                return new Promise((resolve, reject) => {
                    apiGetAccountSelectList()
                        .then((response) => {
                            this.accountSelectList = response.data;
                            console.log("initAccountSelectList載入完成");
                            resolve("initAccountSelectList載入完成");
                        })
                        .catch((error) => {
                            console.log(error);
                            reject();
                        })
                });
            },
            //書籍列表清單
            initBookSelectList() {
                return new Promise((resolve, reject) => {
                    apiGetBookSelectList()
                        .then((response) => {
                            this.bookSelectList = response.data;
                            console.log("initBookSelectList載入完成");
                            resolve("initBookSelectList載入完成");
                        })
                        .catch((error) => {
                            console.log(error);
                            reject();
                        })
                });
            },
            initLoan() {
                return new Promise((resolve, reject) => {
                    apiGetLoan(`/Loan/${this.$route.params.id}`)
                        .then((response) => {
                            this.loan = response.data;
                            console.log("initLoan載入完成");
                            resolve("initLoan載入完成");
                        })
                        .catch((error) => {
                            console.log(error);
                            reject();
                        })
                });

            },
            addLoan() {

                let entity = {
                    AccountId: this.loan.accountId,
                    BookId: this.loan.bookId,
                    IssueDate: this.loan.issueDate,
                    DueDate: this.loan.dueDate,
                    ReturnDate: this.loan.returnDate
                };
                apiPostLoan(entity)
                    .then((response) => {
                        alert("新增成功");
                        //回書籍列表
                        this.$router.push({ name: 'LoanIndex' })
                    })
                    .catch((error) => {
                        console.log(error);

                        let obj = error.response.data.errors;
                        Object.entries(obj).forEach(([key, value]) => {
                            console.log(key, value);
                            alert(value.join(''));
                        });
                    })

            },
            //更新
            updateLoan() {
                let entity = {
                    Id: this.$route.params.id,
                    AccountId: this.loan.accountId,
                    BookId: this.loan.bookId,
                    IssueDate: this.loan.issueDate,
                    DueDate: this.loan.dueDate,
                    ReturnDate: this.loan.returnDate
                };
                apiPutLoan(`/Loan/${this.$route.params.id}`, entity)
                    .then((response) => {
                        alert("更新成功");
                        //回書籍列表
                        this.$router.push({ name: 'LoanIndex' })
                    })
                    .catch((error) => {
                        console.log(error);
                        console.log(error.response.data.errors);
                    })
            }
        },
        created() {
            //編輯
            if (this.isEdit) {

                this.initBookSelectList()
                    .then(success => {
                        this.initAccountSelectList();
                    })
                    .then(x => {
                        this.initLoan();
                    })
                    .catch((error) => {
                        console.log(error);
                    })

            }
            //新增
            else {
                this.initBookSelectList()
                    .then(success => {
                        this.initAccountSelectList();
                    })
            }
        }
    };
</script>

<style lang="scss" scoped>
</style>