<template>
    <div class="container">
      <div class="page-header">
            <h1>Profil de {{ profil.firstName }} {{ profil.lastName }}</h1>
      </div>
      <p>First Name: {{ profil.firstName }}</p>
      <p>Last Name: {{ profil.lastName }}</p>
      <p>Email: {{ profil.email }}</p>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import AuthService from "../../services/AuthService";
    import UserApiService from '../../services/UserApiService';

  export default {
    data() {
        return {
            profil: {}
        };
    },

    async mounted() {
        this.profilEmail = this.$route.params.email;
        this.profil = await UserApiService.getUserAsync(this.profilEmail);
    },

    methods: {}

  };
</script>