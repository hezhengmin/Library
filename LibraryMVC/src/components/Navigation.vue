<template>
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg " color-on-scroll="500">
        <div class="container-fluid">
            <div class="collapse navbar-collapse justify-content-end">
                <ul class="navbar-nav ml-auto">
                    <li class="nav-item">
                        <!-- 登入帳號Id -->
                        <a class="nav-link" href="">
                            <span class="no-icon">{{UserId}}</span>
                        </a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-danger" href="" @click="signOut">
                            <span class="no-icon">登出</span>
                        </a>
                    </li>
                </ul>
            </div>
        </div>
    </nav>
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