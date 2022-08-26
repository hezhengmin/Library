<template>
    <div>
        <button v-on:click="test">點擊:{{num}}</button>
    </div>
</template>
<script>
    export default {
        name: "login-component",
        data() {
            return {
                num: 1
            };
        },
        methods: {
            test() {
                this.num++;
                console.log(this.num);
            }
        }
    };
</script>

<style>
   
</style>