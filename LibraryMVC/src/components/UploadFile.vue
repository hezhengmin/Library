<template>
    <div class="uploadfile">
        <a href="" @click.prevent="downloadFile()">{{fileName}}</a>
        <a href="" @click.prevent="deleteFile()">刪除</a>
    </div>
</template>
<script>
    export default {
        name: "UploadFile",
        props: {
            //BookPhoto Id
            id: {
                type: String
            },
            uploadFileId: {
                type: String,
                required: true
            },
            //檔名
            fileName: {
                type: String,
                required: true
            },
        },
        data() {
            return {

            }
        },
        methods: {
            //檔案下載
            downloadFile() {
                const method = 'GET';
                const url = `https://localhost:44323/api/UploadFile/Download/${this.uploadFileId}`;

                this.$axios.request({
                    url,
                    method,
                    responseType: 'blob', //important
                })
                    .then(({ data }) => {
                        const downloadUrl = window.URL.createObjectURL(new Blob([data]));
                        const link = document.createElement('a');
                        link.href = downloadUrl;
                        link.setAttribute('download', this.fileName); //any other extension
                        document.body.appendChild(link);
                        link.click();
                        link.remove();
                    });
            },
            //刪除檔案
            deleteFile() {

                this.$axios.delete(`https://localhost:44323/api/BookPhoto/${this.id}`)
                    .then((response) => {
                        if (response.status === 204) {
                            alert("刪除成功");
                            //子層傳父層
                            this.$emit('delete', this.id); 
                        }
                        else {
                            alert("刪除失敗");
                        }

                    })
                    .catch((error) => {
                        console.log(error);
                    })
            }
        },
        created() {
        }
    };
</script>

<style>
</style>