import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/participation";

class ParticipantApiService{
    constructor(){

    }

    async getParticipationAsync(quantityId, userId){
        return await getAsync(`${endpoint}/${quantityId}/${userId}`);
    }

    async createParticipationAsync(model){
        return await postAsync(endpoint, model);
    }

    async updateParticipationAsync(model){
        return await putAsync(`${endpoint}/${model.quantityId}/${model.userId}`, model)
    }

    async deleteParticipationAsync(quantityId, userId){
        return await deleteAsync(`${endpoint}/${quantityId}/${userId}/delete`);
    }
}

export default new ParticipantApiService()