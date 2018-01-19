<template>
    <div class="container">    
      <div class="page-header">
            <h1 v-if="mode == 'create'">Create a present</h1>
            <h1 v-else>Edit your present</h1>
        </div>

        <form @submit="onSubmit($event)">
            <div class="alert alert-danger" v-if="errors.length > 0">
                <b>Les champs suivants semblent invalides : </b>

                <ul>
                    <li v-for="e of errors">{{e}}</li>
                </ul>
            </div>

            <div class="form-group">
                <label class="required">Present Name:</label>
                <input type="text" v-model="present.presentName" class="form-control" required>
            </div>

            <div class="form-group">
                <label>Price</label>
                <input type="text" v-model="present.price" class="form-control">
            </div>

            <div class="form-group">
                <label>Link Present</label>
                <input type="text" v-model="present.linkPresent" class="form-control">
            </div>

            <div class="form-group">
                <label>CategoryPresentId</label>
                <b-dropdown right text="Category Present">
                    <tr v-for="i of categoryPresentList" :key="i.categoryPresentId">
                        <b-dropdown-item-button @click="choseCategory(i.categoryPresentId, i.categoryName)">{{ i.categoryName }}</b-dropdown-item-button>
                    </tr>
                </b-dropdown>
                <input type="text" v-model="this.categoryChosen" class="form-control" disabled>
            </div>

            <button type="submit" class="btn btn-primary">Sauvegarder</button>
        </form>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import PresentApiService from '../../services/PresentApiService';
    import CategoryPresentApiService from '../../services/CategoryPresentApiService';
    import UserApiService from "../../services/UserApiService";
    import AuthService from "../../services/AuthService";

  export default {
    data() {
      return {
        user:{},
        present:{},
        mode: null,
        id: null,
        errors: [],
        categoryPresentList: [],
        categoryChosen: "Choose a category."
      };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        this.categoryPresentList = await CategoryPresentApiService.getCategoryPresentListAsync();

        this.mode = this.$route.params.mode;
        this.id = this.$route.params.id;
        
        if(this.mode == 'edit'){
            try {
                // Here, we use "executeAsyncRequest" action. When an exception is thrown, it is not catched: you have to catch it.
                // It is useful when we have to know if an error occurred, in order to adapt the user experience.
                this.present = await this.executeAsyncRequest(() => PresentApiService.getPresentAsync(this.id));
            }
            catch(error) {
                // So if an exception occurred, we redirect the user to the students list.
                this.$router.replace('/presents');
            }   
        }
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async onSubmit(e){
            e.preventDefault();

            var errors = [];

            if(!this.present.presentName) errors.push("Present Name");
            if(!this.present.price) errors.push("Price");
            if(!this.present.linkPresent) errors.push("Link Present");
            if(!this.present.categoryPresentId) errors.push("Category Present Id");

            this.errors = errors;

            if(errors.length == 0) {
            try {
                if(this.mode == 'create') {
                    this.present.userId = this.user.userId;
                    await this.executeAsyncRequest(() => PresentApiService.createPresentAsync(this.present));
                }
                else {
                    await this.executeAsyncRequest(() => PresentApiService.updatePresentAsync(this.present)); 
                }
                this.$router.replace('/presents');
            }
            catch(error) {
                // Custom error management here.
                // In our application, errors throwed when executing a request are managed globally via the "executeAsyncRequest" action: errors are added to the 'app.errors' state.
                // A custom component should react to this state when a new error is added, and make an action, like showing an alert message, or something else.
                // By the way, you can handle errors manually for each component if you need it...
                }
            }
        },
        choseCategory(categoryIdChosen, categoryNameChosen){
            this.categoryChosen = categoryNameChosen;
            this.present.categoryPresentId = categoryIdChosen;
        }
    }
  };
</script>

<style lang="less">

</style>