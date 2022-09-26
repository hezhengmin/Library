<template>
    <div class="LoanEdit">

        <ValidationObserver ref="observer" v-slot="{ invalid }" tag="form">
            <div class="d-flex justify-content-between">
                <div class="py-2">
                    <h2>{{title}}</h2>
                </div>
                <div class="py-2">
                    <button class="btn btn-primary" :disabled="invalid" type="button" @click="onSubmit">確認</button>
                    <button type="button" @click="$router.go(-1)" class="btn btn-primary">
                        回上一頁
                    </button>
                </div>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            書籍資訊
                        </div>
                        <div class="card-body">
                            
                        </div>
                    </div>
                </div>
            </div>
        
            <div class="d-flex my-2">
                <button class="btn btn-primary me-2" :disabled="invalid" type="button" @click="onSubmit">確認</button>
                <button type="button" @click="$router.go(-1)" class="btn btn-primary">
                    回上一頁
                </button>
            </div>
        </ValidationObserver>
    </div>
</template>
<script>

    export default {
        name: "LoanEdit",
      
        data() {
            return {
                loan: {

                }
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
            onSubmit() {
                
            },
            init() {
                this.$axios.get(`https://localhost:44323/api/Loan/${this.$route.params.id}`)
                    .then((response) => {
                        console.log(response.data);
                        this.loan = response.data;
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
        },
        created() {

            //編輯
            if (this.isEdit) {
                this.init();
            }
            //新增
            else {

            }
        }
    };
</script>

<style lang="scss" scoped>
    
</style>