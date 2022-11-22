<template>
    <!-- 互動視窗(Modal) -->
    <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">{{modalTitle}}</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    {{modalBody}}
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary btn-fill" data-bs-dismiss="modal">關閉</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import modal from "bootstrap/js/dist/modal"
    export default {
        name: 'Modal',
        props: {
            modalTitle: {
                type: String,
                default: ""
            },
            modalBody: {
                type: String,
                required: true, //必填
            },
        },
        computed: {
        },
        methods: {
            methodInChild() {
                //Execute code
                alert("Modal");
            },
        }
    }
</script>

<style>
</style>

