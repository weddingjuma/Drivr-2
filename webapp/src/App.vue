<template>
<div id="app">
    <router-link to="/dashboard">Dashboard</router-link>
    <router-view></router-view>
</div>
</template>

<script>

export default {
    name: 'app',
    data() {
        return {

        }
    },
    mounted() {
        this.$router.push('/dashboard');
    }
}
</script>

<style lang="scss" scoped>

</style>
