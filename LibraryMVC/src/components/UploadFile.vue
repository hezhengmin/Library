<template>
    <div class="uploadfile">
        <a href="" @click.prevent="downloadFile()">{{fileName}}</a>
        <a href="" class="text-decoration-none text-danger"  @click.prevent="deleteFile()">刪除</a>
    </div>
</template>
<script>
    import { apiGetUploadFile, apiDeleteUploadFile } from 'api'
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
                const url = `/UploadFile/Download/${this.uploadFileId}`;

                apiGetUploadFile({
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

                let yes = confirm('你確定刪除檔案嗎？');
                if (!yes) return;

                apiDeleteUploadFile(`/BookPhoto/${this.id}`)
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

<style >

</style>