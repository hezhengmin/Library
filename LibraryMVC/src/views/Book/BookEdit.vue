<template>
    <div>
        <input type="text" v-model="book.title" /><br />
        <input type="text" v-model="book.status" /><br />
        <input type="text" v-model="book.isbn" /><br />
        <input type="text" v-model="book.issn" /><br />
        <input type="text" v-model="book.gpn" /><br />
        <input type="text" v-model="book.publisher" /><br />
        <input type="text" v-model="book.rightCondition" /><br />
        <input type="text" v-model="book.creator" /><br />
        <input type="text" v-model="book.publishDate" /><br />
        <input type="text" v-model="book.edition" /><br />
        <input type="text" v-model="book.cover" /><br />
        <input type="text" v-model="book.classify" /><br />
        <input type="text" v-model="book.gpntype" /><br />
        <input type="text" v-model="book.subject" /><br />
        <input type="text" v-model="book.governance" /><br />
        <input type="text" v-model="book.grade" /><br />
        <input type="text" v-model="book.pages" /><br />
        <input type="text" v-model="book.size" /><br />
        <input type="text" v-model="book.binding" /><br />
        <input type="text" v-model="book.language" /><br />
        <input type="text" v-model="book.introduction" /><br />
        <input type="text" v-model="book.catalog" /><br />
        <input type="text" v-model="book.price" /><br />
        <input type="text" v-model="book.targetPeople" /><br />
        <input type="text" v-model="book.types" /><br />
        <input type="text" v-model="book.attachment" /><br />
        <input type="text" v-model="book.url" /><br />
        <input type="text" v-model="book.duration" /><br />
        <input type="text" v-model="book.numbers" /><br />
        <input type="text" v-model="book.restriction" /><br />
        <input type="text" v-model="book.ceasedDate" /><br />
        <input type="text" v-model="book.authority" /><br />
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

<style>
</style>