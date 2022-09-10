<template>
    <div class="layout">
        <div class="d-flex">
            <div class="page-left ">
                <Sidebar />
            </div>
            <div class="page-right">
                <Navigation />
                <main class="p-4 bg-secondary">
                    <div class="container page-content rounded p-4">
                        <router-view></router-view>
                    </div>
                </main>
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
    .layout {
        .page-left {
            min-height: 100vh;
        }

        .page-right {
            min-width: 0;
            width: 100%;
        }

        main {

            .page-content {
                background-color: white;
                min-height: 100vh;
            }
        }
    }
</style> 