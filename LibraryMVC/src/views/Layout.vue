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
            <div class="content">
                 <router-view></router-view>
            </div>
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