<template>
    <div class="wrapper">
        <div class="sidebar">
            <!-- Sidebar -->
            <Sidebar />
            <!-- End Sidebar -->
        </div>
        <div class="main-panel">
            <!-- 導覽頁 start -->
            <Navigation />
            <!-- 導覽頁 end -->
            <main class="p-4">
                <div class="container page-content rounded p-4">
                    <router-view></router-view>
                </div>
            </main>
        </div>
    </div>
</template>
<script>
    import Navigation from "../../src/components/Navigation.vue";
    import Sidebar from "../../src/components/Sidebar.vue";
    export default {
        name: "Layout",
        components: {
            Navigation,
            Sidebar
        },
    };
</script>

<style lang="scss" scoped>
</style> 