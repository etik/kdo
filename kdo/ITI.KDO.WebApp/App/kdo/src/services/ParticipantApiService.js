import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/participant";

class ParticipantApiService{
    constructor(){

    }

    async getParticipantListAsync(userId, eventId){
        return await getAsync(`${endpoint}/${userId}/${eventId}/getParticipantByUserId`);
    }

    async getParticipantAsync(userId){
        return await getAsync(`${endpoint}/${userId}`);
    }

    async createParticipantAsync(model){
        return await postAsync(endpoint, model);
    }

    async updateParticipantAsync(model){
        return await putAsync(`${endpoint}/${model.userId}`, model)
    }

    async deleteEventAsync(userId) {
        return await deleteAsync(`${endpoint}/${userId}`);
    }
}

export default new ParticipantApiService()