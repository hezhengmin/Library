<template>
    <div class="navigation border bg-light">
        <ul class="nav justify-content-end">
            <li class="nav-item my-auto">
                <!-- 登入帳號Id -->
                <span>{{UserId}}</span>
            </li>
            <li class="nav-item">
                <a href="" class="nav-link text-danger" @click="signOut">登出</a>
            </li>
        </ul>
    </div>
</template>

<script>
    export default {
        name: 'Navigation',
        computed: {
            UserId() {
                return this.$store.getters.getUserId;
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