<template>
    <div class="container">    
        <div class="page-header">
            <h1>Participate to a present</h1>
        </div>

        <b-row>
            <b-col sm="4">
                <b-card
                    tag="article"
                    style="max-width: 16rem; height: 256px;"
                    class="mb-2">
                    <div class="card-text">
                        {{present.presentName}} </br>
                        price : {{present.price}} </br>
                        {{present.linkPresent}} </br>
                        quantity : {{quantity.quantity}} </br>
                        pour : {{recipient.firstName}} {{recipient.lastName}} </br>
                        de : {{nominator.firstName}} {{nominator.lastName}}
                    </div>
                </b-card>
            </b-col>

            <b-col>
                <b-form-input v-model="price"
                    type="text"
                    placeholder="Enter the ammount :"></b-form-input>

                <button @click="createParticipation()" class="btn btn-primary">Participate</button>
            </b-col>
        </b-row>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import PresentApiService from '../../services/PresentApiService';
    import QuantityApiService from '../../services/QuantityApiService';
    import UserApiService from "../../services/UserApiService";    
    import ParticipationApiService from "../../services/ParticipationApiService";
    import AuthService from "../../services/AuthService";

  export default {
    data() {
      return {
        user:{},
        present:{},
        quantity:{},
        nominator:{},
        recipient:{},
        participation:{},
        price: null,
        quantityId: null,
        eventId: null,
      };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        this.quantityId = this.$route.params.qid;
        this.eventId = this.$route.params.eid;

        this.quantity = await this.executeAsyncRequest(() => QuantityApiService.getQuantityAsync(this.quantityId));
        this.present = await this.executeAsyncRequest(() => PresentApiService.getPresentAsync(this.quantity.presentId));
        this.nominator = await this.executeAsyncRequest(() => UserApiService.getUserByIdAsync(this.quantity.nominatorId));
        this.recipient = await this.executeAsyncRequest(() => UserApiService.getUserByIdAsync(this.quantity.recipientId));
    },

    methods: {
        ...mapActions(['executeAsyncRequest']),

        async createParticipation()
        {
            console.log("creation");
            this.participation.quantityId = this.quantityId;
            this.participation.userId = this.user.userId;
            this.participation.eventId = this.eventId;
            this.participation.amountUserPrice = this.price;

            var aux = await this.executeAsyncRequest(() => ParticipationApiService.createParticipationAsync(this.participation));
            console.log("prix : " + aux.amountUserPrice);
        }
    }
  };
</script>

<style lang="less">

</style>