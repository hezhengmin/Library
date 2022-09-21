<template>
    <div class="bookEdit">

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
                            <div class="pb-2">
                                <span class="text-danger">*</span>
                                <label for="title" class="form-label">書名</label>
                                <ValidationProvider v-slot="{ valid, errors }" name="標題" rules="required">
                                    <input id="title" name="title" type="text" v-model="book.title"
                                           :class="[{'is-invalid': valid===false}, 'form-control']" />
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </ValidationProvider>
                            </div>
                            <div class="py-2">
                                <span class="text-danger">*</span>
                                <label for="creator" class="form-label">作者資訊</label>
                                <ValidationProvider v-slot="{ valid, errors }" name="作者資訊" rules="required">
                                    <input id="creator" name="creator" type="text" v-model="book.creator"
                                           :class="[{'is-invalid': valid===false}, 'form-control']" />
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </ValidationProvider>
                            </div>
                            <div class="py-2">
                                <span class="text-danger">*</span>
                                <label for="status" class="form-label">狀態</label>
                                <ValidationProvider v-slot="{ valid, errors }" name="狀態" rules="required">
                                    <select id="status" name="status" v-model="book.status" :class="[{'is-invalid': valid===false}, 'form-control']">
                                        <option value="">請選擇</option>
                                        <option value="0">有庫存</option>
                                        <option value="1">無庫存</option>
                                    </select>
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </ValidationProvider>
                            </div>
                            <div class="py-2">
                                <label for="isbn" class="form-label">ISBN</label>
                                <input id="isbn" name="isbn" type="text" v-model="book.isbn" class="form-control" />
                            </div>
                            <div class="py-2">
                                <label for="issn" class="form-label">ISSN</label>
                                <input id="issn" name="issn" class="form-control" type="text" v-model="book.issn" />
                            </div>
                            <div class="py-2">
                                <span class="text-danger">*</span>
                                <label for="gpn" class="form-label">GPN(政府出版品統一編號)</label>
                                <ValidationProvider v-slot="{ valid, errors }" name="GPN" rules="required|digits:10">
                                    <input id="gpn" name="gpn" type="text" v-model="book.gpn"
                                           :class="[{'is-invalid': valid===false}, 'form-control']" />
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </ValidationProvider>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="card">
                        <div class="card-header">
                            圖片檔案
                        </div>
                        <div class="card-body">
                            <carousel :perPage="1" :navigationEnabled="true">
                                <slide v-for="photo in book.bookPhotos" :key="photo.id">
                                    <img :src="'https://localhost:44323/StaticFiles/' + photo.uploadFileId + photo.extension" class="bookPhoto img-thumbnail  rounded mx-auto d-block" :alt="photo.name">
                                    <h2 class="text-center">{{photo.fileCompleteName}}</h2>
                                </slide>
                            </carousel>
                            <div class="input-group">
                                <input class="form-control" name="files" type="file" multiple>
                                <button class="btn btn-primary" type="button">上傳</button>
                            </div>
                            <upload-file v-for="photo in book.bookPhotos" :key="photo.uploadFileId"
                                         :id="photo.id"
                                         :uploadFileId="photo.uploadFileId"
                                         :file-name="photo.fileCompleteName"
                                         @delete="deleteUploadFile"></upload-file>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card mt-4">
                <div class="card-header">
                    出版資訊
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <span class="text-danger">*</span>
                            <label for="publisher" class="form-label">出版單位</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="出版單位" rules="required">
                                <input id="publisher" name="publisher" type="text" v-model="book.publisher"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                        <div class="col-md-6">
                            <span class="text-danger">*</span>
                            <label for="publishDate" class="form-label">出版日期</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="出版日期" rules="required">
                                <input id="publishDate" name="publishDate" type="text" v-model="book.publishDate"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                    </div>
                    <div class="d-block py-3"></div>
                    <div class="row">
                        <div class="col-md-6">
                            <span class="text-danger">*</span>
                            <label for="rightCondition" class="form-label">出版情況</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="出版情況" rules="required">
                                <input id="rightCondition" name="rightCondition" type="text" v-model="book.rightCondition"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                        <div class="col-md-6">
                            <span class="text-danger">*</span>
                            <label for="gpntype" class="form-label">出版品分類</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="出版品分類" rules="required">
                                <input id="gpntype" name="gpntype" type="text" v-model="book.gpntype"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                    </div>
                    <div class="d-block py-3"></div>
                    <div class="row">
                        <div class="col-md-6">
                            <label for="edition" class="form-label">版次</label>
                            <input id="edition" name="edition" class="form-control" type="text" v-model="book.edition" />
                        </div>
                        <div class="col-md-6">
                            <label for="url" class="form-label">出版品網址-線上版或試閱版</label>
                            <input id="url" name="url" class="form-control" type="text" v-model="book.url" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-4">
                <div class="card-header">
                    分類資訊
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label for="cover" class="form-label">書封連結</label>
                            <input id="cover" name="cover" class="form-control" type="text" v-model="book.cover" />
                        </div>
                        <div class="col-md-6">
                            <label for="classify" class="form-label">書籍分類</label>
                            <input id="classify" name="classify" class="form-control" type="text" v-model="book.classify" />
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-md-6">
                            <label for="subject" class="form-label">主題分類</label>
                            <input id="subject" name="subject" class="form-control" type="text" v-model="book.subject" />
                        </div>
                        <div class="col-md-6">
                            <label for="governance" class="form-label">施政分類</label>
                            <input id="governance" name="governance" class="form-control" type="text" v-model="book.governance" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="card mt-4">
                <div class="card-header">
                    其他資訊
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <span class="text-danger">*</span>
                            <label for="price" class="form-label">價格</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="價格" rules="required">
                                <div class="input-group">
                                    <div class="input-group-text">$</div>
                                    <input id="number" name="price" type="number" v-model="book.price"
                                           :class="[{'is-invalid': valid===false}, 'form-control']" />
                                    <span class="invalid-feedback">{{ errors[0] }}</span>
                                </div>
                            </ValidationProvider>
                        </div>
                        <div class="col-md-4">
                            <span class="text-danger">*</span>
                            <label for="pages" class="form-label">頁數</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="頁數" rules="required">
                                <input id="pages" name="pages" type="number" v-model="book.pages"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                        <div class="col-md-4">
                            <label for="size" class="form-label">開數</label>
                            <input id="size" name="size" class="form-control" type="text" v-model="book.size" />
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-md">

                            <span class="text-danger">*</span>
                            <label for="duration" class="form-label">播放時間長度</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="播放時間長度" rules="required">
                                <input id="duration" name="duration" type="text" v-model="book.duration"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                        <div class="col-md">
                            <span class="text-danger">*</span>
                            <label for="language" class="form-label">語言</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="語言" rules="required">
                                <input id="language" name="language" type="text" v-model="book.language"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                        <div class="col-md">
                            <span class="text-danger">*</span>
                            <label for="authority" class="form-label">授權資訊</label>
                            <ValidationProvider v-slot="{ valid, errors }" name="授權資訊" rules="required">
                                <input id="authority" name="authority" type="text" v-model="book.authority"
                                       :class="[{'is-invalid': valid===false}, 'form-control']" />
                                <span class="invalid-feedback">{{ errors[0] }}</span>
                            </ValidationProvider>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-md">
                            <label for="types" class="form-label">資料類型</label>
                            <input id="types" name="types" class="form-control" type="text" v-model="book.types" />
                        </div>
                        <div class="col-md">
                            <label for="attachment" class="form-label">附件</label>
                            <input id="attachment" name="attachment" class="form-control" type="text" v-model="book.attachment" />
                        </div>
                        <div class="col-md">
                            <label for="targetPeople" class="form-label">適用對象</label>
                            <input id="targetPeople" name="targetPeople" class="form-control" type="text" v-model="book.targetPeople" />
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-md">
                            <label for="grade" class="form-label">級別</label>
                            <input id="grade" name="grade" class="form-control" type="text" v-model="book.grade" />
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
                    <div class="row mt-4">
                        <div class="col-md">
                            <label for="ceasedDate" class="form-label">停刊註記</label>
                            <input id="ceasedDate" name="ceasedDate" class="form-control" type="datetime" v-model="book.ceasedDate" />
                        </div>
                        <div class="col-md">
                            <label for="binding" class="form-label">裝訂</label>
                            <input id="binding" name="binding" class="form-control" type="text" v-model="book.binding" />
                        </div>
                        <div class="col-md">
                        </div>
                    </div>
                </div>
            </div>
            <div class="card mt-4">
                <div class="card-header">
                    目次、作者簡介
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6">
                            <label for="catalog" class="form-label">目次</label>
                            <textarea id="catalog" name="catalog" class="form-control" type="text" v-model="book.catalog" rows="5" />
                        </div>
                        <div class="col-md-6">
                            <label for="introduction" class="form-label">書籍介紹</label>
                            <textarea id="introduction" name="introduction" class="form-control" type="text" v-model="book.introduction" rows="5" />
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
    import UploadFile from "../../../src/components/UploadFile.vue";
    import { Carousel, Slide } from 'vue-carousel';

    export default {
        name: "BookEdit",
        components: {
            UploadFile,
            Carousel,
            Slide
        },
        data() {
            return {
                book: {
                    title: "",
                    status: "",
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
                    price: 0,
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

                const formData = new FormData(this.$refs.observer.$el);

                formData.append("id", this.$route.params.id);



                this.$axios.put(`https://localhost:44323/api/Book/${this.$route.params.id}`,
                    formData,
                    {
                        headers: {
                            'Content-Type': 'multipart/form-data;',
                        }
                    })
                    .then((response) => {
                        if (response.status === 204) {
                            alert("更新成功");
                            this.$router.push({ name: 'BookIndex' })
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

                this.$axios.post('https://localhost:44323/api/Book',
                    formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data;',
                    }
                })
                    .then((response) => {
                        alert("新增成功");
                        //回書籍列表
                        this.$router.push({ name: 'BookIndex' })
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            init() {
                this.$axios.get(`https://localhost:44323/api/Book/${this.$route.params.id}`)
                    .then((response) => {
                        this.book = { ...response.data };
                    })
                    .catch((error) => {
                        console.log(error);
                    })
            },
            //刪除上傳的圖片
            deleteUploadFile(id) {
                //BookPhoto的id
                const isDeleteBookPhotoIndex = this.book.bookPhotos.findIndex(x => x.id === id);
                this.book.bookPhotos.splice(isDeleteBookPhotoIndex, 1);
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

<style lang="scss" scoped>
    .bookPhoto {
        max-height: 200px;
    }
</style>