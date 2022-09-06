<template>
    <div class="container-fluid">

        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="title" class="form-control" type="text" v-model="book.title" />
                    <label for="title" class="form-label">書名</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="status" class="form-control" type="text" v-model="book.status" />
                    <label for="status" class="form-label">狀態</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="isbn" class="form-control" type="text" v-model="book.isbn" />
                    <label for="isbn" class="form-label">ISBN</label>
                </div>
            </div>
        </div>

        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="issn" class="form-control" type="text" v-model="book.issn" />
                    <label for="issn" class="form-label">ISSN</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="gpn" class="form-control" type="text" v-model="book.gpn" />
                    <label for="gpn" class="form-label">GPN</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="publisher" class="form-control" type="text" v-model="book.publisher" />
                    <label for="publisher" class="form-label">出版單位</label>
                </div>
            </div>
        </div>

        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="rightCondition" class="form-control" type="text" v-model="book.rightCondition" />
                    <label for="rightCondition" class="form-label">出版情況</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="creator" class="form-control" type="text" v-model="book.creator" />
                    <label for="creator" class="form-label">作者資訊</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="publishDate" class="form-control" type="text" v-model="book.publishDate" />
                    <label for="publishDate" class="form-label">出版日期</label>
                </div>
            </div>
        </div>

        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="edition" class="form-control" type="text" v-model="book.edition" />
                    <label for="edition" class="form-label">版次</label>
                </div>
            </div>
            <div class="col-md">

                <div class="form-floating">
                    <input id="cover" class="form-control" type="text" v-model="book.cover" />
                    <label for="cover" class="form-label">書封連結</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="classify" class="form-control" type="text" v-model="book.classify" />
                    <label for="classify" class="form-label">書籍分類</label>
                </div>
            </div>
        </div>

        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="gpntype" class="form-control" type="text" v-model="book.gpntype" />
                    <label for="gpntype" class="form-label">出版品分類</label>
                </div>
            </div>
            <div class="col-md">

                <div class="form-floating">
                    <input id="subject" class="form-control" type="text" v-model="book.subject" />
                    <label for="subject" class="form-label">主題分類</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="governance" class="form-control" type="text" v-model="book.governance" />
                    <label for="governance" class="form-label">施政分類</label>
                </div>
            </div>
        </div>

        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="grade" class="form-control" type="text" v-model="book.grade" />
                    <label for="grade" class="form-label">級別</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="pages" class="form-control" type="number" v-model="book.pages" />
                    <label for="pages" class="form-label">頁數</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="size" class="form-control" type="text" v-model="book.size" />
                    <label for="size" class="form-label">開數</label>
                </div>
            </div>
        </div>

        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="binding" class="form-control" type="text" v-model="book.binding" />
                    <label for="binding" class="form-label">裝訂</label>
                </div>
            </div>
            <div class="col-md">

                <div class="form-floating">
                    <input id="language" class="form-control" type="text" v-model="book.language" />
                    <label for="language" class="form-label">語言</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <textarea id="introduction" class="form-control" type="text" v-model="book.introduction" />
                    <label for="introduction" class="form-label">書籍介紹</label>
                </div>
            </div>
        </div>
        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <textarea id="catalog" class="form-control" type="text" v-model="book.catalog" />
                    <label for="catalog" class="form-label">目次</label>
                </div>
            </div>
            <div class="col-md">

                <label for="price" class="form-label">價格</label>
                <div class="input-group">
                    <div class="input-group-text">$</div>
                    <input type="number" id="price" class="form-control" v-model="book.price" />
                </div>
            </div>
            <div class="col-md">

                <div class="form-floating">
                    <input id="targetPeople" class="form-control" type="text" v-model="book.targetPeople" />
                    <label for="targetPeople" class="form-label">適用對象</label>
                </div>
            </div>
        </div>
        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="types" class="form-control" type="text" v-model="book.types" />
                    <label for="types" class="form-label">資料類型</label>
                </div>
            </div>
            <div class="col-md">

                <div class="form-floating">
                    <input id="attachment" class="form-control" type="text" v-model="book.attachment" />
                    <label for="attachment" class="form-label">附件</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="url" class="form-control" type="text" v-model="book.url" />
                    <label for="url" class="form-label">出版品網址-線上版或試閱版</label>
                </div>
            </div>
        </div>
        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="duration" class="form-control" type="text" v-model="book.duration" />
                    <label for="duration" class="form-label">播放時間長度</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="numbers" class="form-control" type="text" v-model="book.numbers" />
                    <label for="numbers" class="form-label">字號</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="restriction" class="form-control" type="text" v-model="book.restriction" />
                    <label for="restriction" class="form-label">權利範圍</label>
                </div>
            </div>
        </div>
        <div class="row g-2">
            <div class="col-md">
                <div class="form-floating">
                    <input id="ceasedDate" class="form-control" type="datetime" v-model="book.ceasedDate" />
                    <label for="ceasedDate" class="form-label">停刊註記</label>
                </div>
            </div>
            <div class="col-md">
                <div class="form-floating">
                    <input id="authority" class="form-control" type="text" v-model="book.authority" />
                    <label for="authority" class="form-label">授權資訊</label>
                </div>
            </div>
            <div class="col-md">
            </div>
        </div>

        <button @click="updateBook">確認</button>
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
        methods: {
            updateBook() {

            }
        },
        created() {

            this.$axios.get(`https://localhost:44323/api/Book/${this.$route.params.id}`)
                .then((response) => {
                    console.log(response.data);

                    this.book = { ...response.data };
                })
                .catch((error) => {
                    console.log(error);
                })
        }
    };
</script>

<style lang="scss">

    #introduction {
        height: 100px;
    }

    #catalog {
        height: 100px;
    }
</style>