<template>
<div>
<section>
  <div class="title">
    <h1>YOUR EVENT</h1>
  </div>
</section>
<b-row>
    <b-col md="4">
        <b-card style="margin-left: 10%;"   class="text-center"header-bg-variant="danger" bg-variant="light" header="Informations">
            <h6 slot="header"class="mb-0" text-variant="white">INFORMATIONS</h6>
            <b-col sm="3" >
                <b-img rounded blank width="90" height="90" blank-color="#777" alt="img" class="m-1" />
            </b-col>
            <b-col sm="12">
            <router-link tag="img" class="edite" src="https://image.flaticon.com/icons/svg/84/84380.svg" :to="`/events/edit/${eventId}`"></router-link>
            <div class="Info">{{event.eventName}}
            <br>Date
            </div>
            </b-col>
        </b-card>
    <br>
        <b-card style="margin-left:10%;" class="text-center" bg-variant="light" header="PARTICIPANTS">
            <h6 slot="header"class="mb-0">PARTICIPANTS</h6>
                <b-card v-for="i in participantUserList"
                    tag="article"
                    style="max-width: 16rem;margin-left:23%;"
                    class="mb-2">
                    {{i.firstName}}
                    {{i.lastName}}
                 </b-card>
        </b-card>
        </b-col>
    <b-col md="8">
        <b-card class="text-center" bg-variant="light" header="PRESENT">
        <h6 slot="header"class="mb-0" color="white">PRESENTS</h6>
        <div class="row" style="margin-left:11%;">
                <div md="12" class="feature-box event"  v-for="i in quantityPresentList">
                <div class="test-event">
                <span>{{ i.presentName }}</span>
                </div>
                <div class="btni">
                <router-link tag="img" src="https://image.flaticon.com/icons/svg/84/84380.svg"  class="editP" :to="`/events/presents/edit/${eventId}/${i.quantityId}`" v-if="i.nominatorId == user.userId"></router-link>
                <router-link tag="img" src="https://cdn2.iconfinder.com/data/icons/sales-and-delivery/128/easy-2-512.png"  class="participant" :to="`/events/participate/${eventId}/${i.quantityId}`"></router-link>
                </div>
                </div>
            </div>
        </b-card>
    </b-col>
     </b-row>
  </div>
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
            beneficiary: [],
            participantUserList: [],
            quantityList: [],
            selected: null,
            quantityPresentList: []
        };
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.eventId = this.$route.params.id;
        this.event = await this.executeAsyncRequest(() => EventApiService.getEventAsync(this.eventId));
        
        this.user = await UserApiService.getUserAsync(userEmail);
        
        await this.refreshList();
        await this.refreshParticipantList();
        this.selected = this.beneficiary[0].value;
        await this.refreshQuantityList();
    },

    methods: {
        ...mapActions(['executeAsyncRequestOrDefault', 'executeAsyncRequest']),

        async refreshList() {
            this.event = await EventApiService.getEventAsync(this.eventId);
        },
        async refreshParticipantList(){
            this.participantUserList = [];
            this.beneficiary = [];
            var aux;
            this.participantList = await ParticipantApiService.getParticipantListAsync(this.user.userId, this.eventId);
            
            for(var i = 0; i < this.participantList.length; i++)
            {
                aux = await UserApiService.getUserByIdAsync(this.participantList[i].userId);

                this.participantUserList.push(aux);
                if (this.participantList[i].participantType == true)                    
                    this.beneficiary.push({value: aux.userId, text: aux.firstName});
            }
        },
        async refreshQuantityList(){
            this.quantityList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityListAsync(this.eventId));
            this.quantityPresentList = await this.executeAsyncRequest(() => QuantityApiService.getQuantityPresentListAsync(this.selected, this.eventId));
            if (this.selected == this.user.userId)
            {
                for (var i = 0; i < this.quantityPresentList.length; i++)
                {
                    if (this.quantityPresentList[i].nominatorId != this.user.userId)
                    {
                        this.quantityPresentList.splice(i, 1);
                        i--;
                    }
                }
            }
        }
    }
  };
</script>

<style lang="less">
.row {
    margin-top:10%;
}
.Info {
    font-size: xx-large;
    font-variant: all-petite-caps;
    margin-top: -28%;

}
.btni {
    margin-top:-36%;
}
.edite {
    width: 7%;
    margin-left: 98%;
    margin-top: -70%;
}
.bordered {
    border-style: solid;
    border-width: 1px;
}
.editP{
    width:15%;
}
.participant{
    width:16%;
}
.card-header.bg-danger {
    background-color:#db7070 !important;
}
</style>