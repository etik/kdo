<template>
    <div>
        <b-card  bg-variant="light" text-variant="dark">
            <p class="card-text">
                <img :src="item.photo" /> <br />
                {{auth.email}} <br />
                {{item.firstName}} <br />
                {{item.lastName}} <br />
                {{item.birthdate}} <br />
                {{item.phone}} <br />
            </p>
            <b-button href="/Home/contact" variant="primary">Contact</b-button>
        </b-card>
    </div>
</template>
<script>
    import AuthService from "../../services/AuthService";
    import UserApiService from "../../services/UserApiService";
    import { mapGetters, mapActions } from "vuex";
    import "../../directives/requiredProviders";
    import Vue from 'vue';
    import Vuex from 'vuex';

    export default {
        data() {
            return {
                userEmail: null,
                item: {}
            };
        },

        computed: {
            ...mapGetters(["isLoading"]),
            auth: () => AuthService
        },

        async mounted() {
            var userEmail = AuthService.emailUser();
            this.item = await UserApiService.getUserAsync(userEmail);
        },

        methods: {
            modifyPassword(){
                AuthService.modifyPassword();
            },
        }
    };
</script>

<style lang="less" scoped>
.progress {
    margin: 0px;
    padding: 0px;
    height: 5px;
}

a.router-link-active {
    font-weight: bold;
}
</style>

<style lang="less">
    @import "../../styles/global.less";
</style>