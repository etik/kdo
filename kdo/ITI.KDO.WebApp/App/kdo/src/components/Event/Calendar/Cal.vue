<template>
    <div id="app"
         class="demo">
        <h1 class="demo-title">Calendar</h1>
        <div class="demo-container">
            <scheduleCalendar :originData="eventList"></scheduleCalendar>
        </div>
    </div>
</template>
<script>
import scheduleCalendar from './scheduleCalendar'
import EventApiService from '../../../services/EventApiService'
import UserApiService from '../../../services/UserApiService'
import AuthService from '../../../services/AuthService'

export default {
    name: 'app',
    components: {
        scheduleCalendar
    },

    data() {
        return {
            eventList: [
                {
                    id: 111,
                    date: '2018-01-15',
                    text: "event1"
                },
                {
                    id: 222,
                    date: '2018-01-15',
                    text: "event2"
                },
            ],
            user: {}
        }
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);

        await this.refreshList();
    },
    
    methods: {
        async refreshList() {
            //this.eventList = await EventApiService.getEventSuggestListByUserIdAsync(this.user.userId);
            //console.log(eventList);
        }
    }
}
</script>
<style lang="less">
html {
    background: #eee
}

body,
html {
    height: 100%;
    overflow: hidden;
}

body {
    margin: 0;
}

.demo {
    display: flex;
    flex-direction: column;
    height: 100%;
    padding: 0 30px 30px;
    box-sizing: border-box
}

.demo-title {
    text-align: center
}

.demo-container {
    flex: 1
}
</style>