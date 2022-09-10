<template>
    <div class="navigation border">
        <ul class="nav justify-content-end">
            <li class="nav-item my-auto">
                <!-- 登入帳號Id -->
                <span>{{AccountId}}</span>
            </li>
            <li class="nav-item">
                <a href="" class="nav-link" @click="signOut">登出</a>
            </li>
        </ul>
    </div>
</template>

<script>
    export default {
        name: 'Navigation',
        computed: {
            AccountId() {
                return this.$store.getters.getAccountId;
            }
        },
        methods: {
            signOut() {
                localStorage.clear();
            }
        }
    }
</script>

<style>
</style>