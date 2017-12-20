<template>
    <div class="container">
        <div class="page-header" style="margin-bottom:-64px;">
                <h1>Events</h1>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <b-card-group>
                <b-img rounded blank width="120" height="150" blank-color="#777" alt="img" class="m-1" />
                    <div class="col-sm-6">
                        <b-list-group>
                        <b-list-group-item>
                            {{event.eventName}}
                        </b-list-group-item>
                        <b-list-group-item>
                            Date
                        </b-list-group-item>
                        </b-list-group>
                    
                        <b-button :to="`/events/edit/${eventId}`" variant="primary"> Edit </b-button>
                    </div>
                </b-card-group>
            </div>
        </div>

        <div class="carou">
            <b-card
                    tag="article"
                    style="max-width: 16rem;"
                    class="mb-2">
            <h2 class="card-text" href="#">
               oui
            </h2>
            </b-card>
        </div>
    </div>
</template>

<script>
    import { mapActions } from 'vuex';
    import ParticipantApiService from '../../services/ParticipantApiService';
    import AuthService from "../../services/AuthService";
    import EventApiService from '../../services/EventApiService';
    import UserApiService from '../../services/UserApiService';

  export default {
    data() {
        return {
            user: {},
            eventId: null,
            event: {},
            participantList: []
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventAsync(this.eventId));

        
        this.user = await UserApiService.getUserAsync(userEmail);

        console.log(this.user.userId);
        console.log(this.eventId);
        
        await this.refreshList();
        await this.refreshParticipantList();
        console.log(this.participantList);
    },

    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshList() {
            this.event = await EventApiService.getEventAsync(this.eventId);
      },
       async refreshParticipantList(){
            this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);
      },
    }
  };
</script>

<style lang="less">
.row {
    margin-top:10%;
}
.carou {
    width: 100px;
    //overflow-x: scroll;
    //background: grey;
    display: block; 
}

</style>