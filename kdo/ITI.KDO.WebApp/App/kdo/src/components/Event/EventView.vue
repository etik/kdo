<template>
    <div class="container">
      <div class="page-header">
            <h1>Events</h1>
      </div>


      <table class="table table-striped table-hover table-bordered">
            <thead>
                <tr>
                    <th>Participant</th>
                    <th>userId</th>
                    <th>eventId</th>
                </tr>
            </thead>

            <tbody>

                <tr v-if="participantList.length == 0">
                    <td colspan="7" class="text-center">Participant</td>
                </tr>

                <tr v-for="i of participantList">
                    <td>{{ i.ParticipantType }}</td>
                    <td>{{ i.userId }}</td>
                    <td>{{ i.eventId }}</td>
                </tr>
            </tbody>
        </table>

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
            event: [],
            participantList: []
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.eventId = this.$route.params.id;
        
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

</style>