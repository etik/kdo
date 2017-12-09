import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/notification"

class NotificationApiService{
    constructor(){

    }
    
    async getNotificationListAsync(userId){
        return await getAsync(`${endpoint}/${userId}/getAll`);
    }
}

export default new NotificationApiService()