import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/notification"

class NotificationApiService{
    constructor(){

    }
    
    async getNotificationListAsync(){
        return await getAsync(`${endpoint}/${userId}/getNotificationByUserId`);
    }

    async getNotificationAsync(notificationId){
        return await getAsync(`${endpoint}/${notificationId}`);
    }

    async createNotificationAsync(model){
        return await postAsync(endpoint, model);
    }

    async updateNotification(model){
        return await putAsync(`${endpoint}/${model.presentId}`, model);
    }

    async deleteNotificationAsync(notificationId){
        return await deleteAsync(`${endpoint}/${presentId}`);
    }
}

export default new NotificationApiService()