<template>
    <b-col style="height:100%; margin-top:-64px;" class="bv-example-row">
        <b-row>
            <b-col sm="3" class="bordered">
                <b-img rounded blank width="120" height="150" blank-color="#777" alt="img" class="m-1" />

                <b-list-group>
                    <b-list-group-item>{{event.eventName}}</b-list-group-item>
                    <b-list-group-item>Date</b-list-group-item>
                </b-list-group>
                <b-button :to="`/events/edit/${eventId}`" variant="primary"> Edit </b-button>
            </b-col>

            <b-col sm="9" class="bordered">
                <b-row>
                    <b-col sm="2">
                        Present for :
                    </b-col>
                    <b-col sm="10">
                        <b-form-select v-model="selected" :options="participantList" class="mb-3">
                        </b-form-select>
                    </b-col>
                </b-row>
                <b-row class="bordered">
                    <b-card
                            tag="article"
                            style="max-width: 16rem; height: 256px;"
                            class="mb-2">
                        <h2 class="card-text" href="#">
                            <b-dropdown id="ddown1" text="Add a present" class="m-md-2">
                                <b-dropdown-item href="#">Create a new present</b-dropdown-item>
                                <b-dropdown-item :to="`/events/importPresent/${eventId}`">Import from your list of present</b-dropdown-item>
                            </b-dropdown>
                        </h2>
                    </b-card>
                    <b-card v-for="i in quantityList"
                            tag="article"
                            style="max-width: 16rem; Sheight: 256px;"
                            class="mb-2">
                        <h2 class="card-text" href="#">
                            {{getPresent(i.presentId)}}                
                        </h2>
                    </b-card>
                </b-row>
            </b-col>
        </b-row>

        <b-row class="bordered">
            <b-card v-for="i in participantList"
                    tag="article"
                    style="max-width: 16rem;"
                    class="mb-2">
                <h2 class="card-text" href="#">
                    {{i.firstname}}
                    {{i.lastname}}
                </h2>
            </b-card>
        </b-row>
    </b-col>
</template>

<script>
    import { mapActions } from 'vuex';
    import ParticipantApiService from '../../services/ParticipantApiService';
    import AuthService from "../../services/AuthService";
    import EventApiService from '../../services/EventApiService';
    import UserApiService from '../../services/UserApiService';
    import QuantityApiService from '../../services/QuantityApiService';
    import PresentApiService from '../../services/PresentApiService';

  export default {
    data() {
        return {
            user: {},
            eventId: null,
            event: {},
            participantList: [],
            quantityList: [],
            selected: null
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventAsync(this.eventId));
        
        this.user = await UserApiService.getUserAsync(userEmail);
        
        await this.refreshList();
        await this.refreshParticipantList();
        await this.refreshQuantityList();
    },

    methods: {
      ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

      async refreshList() {
            this.event = await EventApiService.getEventAsync(this.eventId);
      },
      async refreshParticipantList(){
            this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);
      },
      async refreshQuantityList(){
            this.quantityList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityListAsync(this.eventId));
      },
      async getPresent(id){
           var present;
            present = PresentApiService.getPresentAsync(id);
                return present.presentName;
      }
    }
  };
</script>

<style lang="less">
.row {
    margin-top:10%;
}

.bordered {
    border-style: solid;
    border-width: 1px;
}

</style>