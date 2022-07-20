<template>
    <div id="login" class="login g-2">
        <div class="row align-items-center mt-5">
            <div class="col">
            </div>
            <div class="col">
                <div class="row g-2 bg-light p-3">
                    <div class="col-12">
                        <label for="acc" class="form-label">帳號</label>
                        <input id="acc" type="text" class="form-control" />
                    </div>
                    <div class="col-12">
                        <label for="pwd" class="form-label">密碼</label>
                        <input id="pwd" type="password" class="form-control" />
                    </div>
                    <div class="col-12">
                        <div class="d-flex justify-content-center mt-2">
                            <button type="submit" class="btn btn-primary">登入</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col">
            </div>
        </div>
    </div>
</template>
<script>
    export default {
        name: "login-component",
    };
</script>

<style lang="scss">
    body {
        background: url('../../assert/account/img/signin.jpg') no-repeat;
    }
</style>