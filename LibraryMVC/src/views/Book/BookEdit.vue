<template>
    <div class="bookEdit">

        <ValidationObserver ref="observer" v-slot="{ invalid }" tag="form" @submit.prevent="onSubmit">
            <div class="d-flex justify-content-between">
                <div class="py-2">
                    <h2>{{title}}</h2>
                </div>
                <div class="py-2">
                    <button class="btn btn-primary" :disabled="invalid" type="submit">確認</button>
                    <button type="button" @click="$router.go(-1)" class="btn btn-primary">
                        回上一頁
                    </button>
                </div>
            </div>
            <div class="row g-2">
                <div class="col-md-4">
                    <label for="title" class="form-label">書名</label>
                    <ValidationProvider v-slot="{ valid, errors }" name="標題" rules="required">
                        <input id="title" name="title" type="text" v-model="book.title"
                               :class="[{'is-invalid': valid===false}, 'form-control']" />
                        <span class="invalid-feedback">{{ errors[0] }}</span>
                    </ValidationProvider>
                </div>
                <div class="col-md-4">
                    <label for="status" class="form-label">狀態</label>
                    <input id="status" name="status" class="form-control" type="text" v-model="book.status" />
                </div>
                <div class="col-md-4">
                    <label for="isbn" class="form-label">ISBN</label>
                    <input id="isbn" name="isbn" class="form-control" type="text" v-model="book.isbn" />
                </div>
            </div>
            <div class="row g-2">
                <div class="col-md-4">
                    <label for="issn" class="form-label">ISSN</label>
                    <input id="issn" name="issn" class="form-control" type="text" v-model="book.issn" />
                </div>
                <div class="col-md-4">
                    <label for="gpn" class="form-label">GPN</label>
                    <input id="gpn" name="gpn" class="form-control" type="text" v-model="book.gpn" />
                </div>
                <div class="col-md-4">
                    <label for="publisher" class="form-label">出版單位</label>
                    <input id="publisher" name="publisher" class="form-control" type="text" v-model="book.publisher" />
                </div>
            </div>

            <div class="row g-2">
                <div class="col-md-4">
                    <label for="rightCondition" class="form-label">出版情況</label>
                    <input id="rightCondition" name="rightCondition" class="form-control" type="text" v-model="book.rightCondition" />
                </div>
                <div class="col-md-4">
                    <label for="creator" class="form-label">作者資訊</label>
                    <input id="creator" name="creator" class="form-control" type="text" v-model="book.creator" />
                </div>
                <div class="col-md-4">
                    <label for="publishDate" class="form-label">出版日期</label>
                    <input id="publishDate" name="publishDate" class="form-control" type="text" v-model="book.publishDate" />
                </div>
            </div>

            <div class="row g-2">
                <div class="col-md-4">
                    <label for="edition" class="form-label">版次</label>
                    <input id="edition" name="edition" class="form-control" type="text" v-model="book.edition" />
                </div>
                <div class="col-md-4">
                    <label for="cover" class="form-label">書封連結</label>
                    <input id="cover" name="cover" class="form-control" type="text" v-model="book.cover" />
                </div>
                <div class="col-md-4">
                    <label for="classify" class="form-label">書籍分類</label>
                    <input id="classify" name="classify" class="form-control" type="text" v-model="book.classify" />
                </div>
            </div>

            <div class="row g-2">
                <div class="col-md-4">
                    <label for="gpntype" class="form-label">出版品分類</label>
                    <input id="gpntype" name="gpntype" class="form-control" type="text" v-model="book.gpntype" />
                </div>
                <div class="col-md-4">
                    <label for="subject" class="form-label">主題分類</label>
                    <input id="subject" name="subject" class="form-control" type="text" v-model="book.subject" />
                </div>
                <div class="col-md-4">
                    <label for="governance" class="form-label">施政分類</label>
                    <input id="governance" name="governance" class="form-control" type="text" v-model="book.governance" />
                </div>
            </div>

            <div class="row g-2">
                <div class="col-md">
                    <label for="grade" class="form-label">級別</label>
                    <input id="grade" name="grade" class="form-control" type="text" v-model="book.grade" />
                </div>
                <div class="col-md">
                    <label for="pages" class="form-label">頁數</label>
                    <input id="pages" name="pages" class="form-control" type="number" v-model="book.pages" />
                </div>
                <div class="col-md">
                    <label for="size" class="form-label">開數</label>
                    <input id="size" name="size" class="form-control" type="text" v-model="book.size" />
                </div>
            </div>
            <div class="row g-2">
                <div class="col-md">
                    <label for="binding" class="form-label">裝訂</label>
                    <input id="binding" name="binding" class="form-control" type="text" v-model="book.binding" />
                </div>
                <div class="col-md">
                    <label for="language" class="form-label">語言</label>
                    <input id="language" name="language" class="form-control" type="text" v-model="book.language" />
                </div>
                <div class="col-md">
                    <label for="introduction" class="form-label">書籍介紹</label>
                    <textarea id="introduction" name="introduction" class="form-control" type="text" v-model="book.introduction" />
                </div>
            </div>
            <div class="row g-2">
                <div class="col-md">
                    <label for="catalog" class="form-label">目次</label>
                    <textarea id="catalog" name="catalog" class="form-control" type="text" v-model="book.catalog" />
                </div>
                <div class="col-md">
                    <label for="price" class="form-label">價格</label>
                    <div class="input-group">
                        <div class="input-group-text">$</div>
                        <input type="number" name="number" id="price" class="form-control" v-model="book.price" />
                    </div>
                </div>
                <div class="col-md">
                    <label for="targetPeople" class="form-label">適用對象</label>
                    <input id="targetPeople" name="targetPeople" class="form-control" type="text" v-model="book.targetPeople" />
                </div>
            </div>
            <div class="row g-2">
                <div class="col-md">
                    <label for="types" class="form-label">資料類型</label>
                    <input id="types" name="types" class="form-control" type="text" v-model="book.types" />
                </div>
                <div class="col-md">
                    <label for="attachment" class="form-label">附件</label>
                    <input id="attachment" name="attachment" class="form-control" type="text" v-model="book.attachment" />
                </div>
                <div class="col-md">
                    <label for="url" class="form-label">出版品網址-線上版或試閱版</label>
                    <input id="url" name="url" class="form-control" type="text" v-model="book.url" />
                </div>
            </div>
            <div class="row g-2">
                <div class="col-md">
                    <label for="duration" class="form-label">播放時間長度</label>
                    <input id="duration" name="duration" class="form-control" type="text" v-model="book.duration" />
                </div>
                <div class="col-md">
                    <label for="numbers" class="form-label">字號</label>
                    <input id="numbers" name="numbers" class="form-control" type="text" v-model="book.numbers" />
                </div>
                <div class="col-md">
                    <label for="restriction" class="form-label">權利範圍</label>
                    <input id="restriction" name="restriction" class="form-control" type="text" v-model="book.restriction" />
                </div>
            </div>
            <div class="row g-2">
                <div class="col-md">
                    <label for="ceasedDate" class="form-label">停刊註記</label>
                    <input id="ceasedDate" name="ceasedDate" class="form-control" type="datetime" v-model="book.ceasedDate" />
                </div>
                <div class="col-md">
                    <label for="authority" class="form-label">授權資訊</label>
                    <input id="authority" name="authority" class="form-control" type="text" v-model="book.authority" />
                </div>
                <div class="col-md">
                    <label for="formFileMultiple" class="form-label">圖片檔案</label>
                    <input class="form-control" name="files" type="file" id="formFileMultiple" multiple>
                    <div class="d-flex" v-for="photo in book.bookPhotos" :key="photo.uploadFileId">
                        <a href="" @click.prevent="download(photo.uploadFileId)">{{photo.uploadFileId}}</a>
                    </div>
                </div>
            </div>
            <div class="d-flex my-2">
                <button class="btn btn-primary" :disabled="invalid" type="submit">確認</button>
                <button type="button" @click="$router.go(-1)" class="btn btn-primary">
                    回上一頁
                </button>
            </div>
        </ValidationObserver>
    </div>
</template>
<script>
    export default {
        name: "BookEdit",
        data() {
            return {
                book: {
                    title: "",
                    status: 0,
                    isbn: "",
                    issn: "",
                    gpn: "",
                    publisher: "",
                    rightCondition: "",
                    creator: "",
                    publishDate: "",
                    edition: "",
                    cover: "",
                    classify: "",
                    gpntype: "",
                    subject: "",
                    governance: "",
                    grade: "",
                    pages: 0,
                    size: "",
                    binding: "",
                    language: "",
                    introduction: "",
                    catalog: "",
                    price: null,
                    targetPeople: "",
                    types: "",
                    attachment: "",
                    url: "",
                    duration: "",
                    numbers: "",
                    restriction: "",
                    ceasedDate: null,
                    authority: ""
                }
            }
        },
        computed: {
            //是否為編輯
            isEdit() {
                return this.$route.params.id !== "00000000-0000-0000-0000-000000000000";
            },
            title() {
                return this.isEdit ? "書籍編輯" : "書籍新增";
            }
        },
        methods: {
            //提交前驗證
            async onSubmit() {
                const isValid = await this.$refs.observer.validate();

                if (isValid) {
                    if (this.isEdit) {
                        this.updateBook(); //編輯書籍
                    }
                    else {
                        this.addBook(); //新增書籍
                    }
                }
                else {

                }
            },
            updateBook() {
                this.$axios.put(`https://localhost:44323/api/Book/${this.$route.params.id}`, {
                    ... this.book
                })
                    .then((response) => {
                        if (response.status === 204) {
                            alert("更新成功");
                        }
                        else {
                            alert("更新失敗");
                        }

                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            addBook() {
                const formData = new FormData(this.$refs.observer.$el);

                // Display the key/value pairs
                for (const pair of formData.entries()) {
                    console.log(`${pair[0]}, ${pair[1]}`);
                }

                this.$axios.post('https://localhost:44323/api/Book',
                    formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data;',
                    }
                })
                    .then((response) => {
                        console.log(response);
                        alert("新增成功");
                        //回書籍列表
                        this.$router.push({ name: 'BookIndex' })
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            download(id) {
               
                const method = 'GET';
                const url = `https://localhost:44323/api/Download/${id}`;

                this.$axios.request({
                        url,
                        method,
                        responseType: 'blob', //important
                    })
                    .then(({ data }) => {
                        const downloadUrl = window.URL.createObjectURL(new Blob([data]));
                        const link = document.createElement('a');
                        link.href = downloadUrl;
                        link.setAttribute('download', '123.jpg'); //any other extension
                        document.body.appendChild(link);
                        link.click();
                        link.remove();
                    });
            },
            init() {
                this.$axios.get(`https://localhost:44323/api/Book/${this.$route.params.id}`)
                    .then((response) => {

                        console.log(response.data);
                        this.book = { ...response.data };

                    })
                    .catch((error) => {
                        console.log(error);
                    })
            }
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

<style>
</style>