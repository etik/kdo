import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/contact";

class ContactApiService{
    constructor(){

    }

    async getContactListAsync(userId){
        return await getAsync(`${endpoint}/${userId}/getContactsByUserId`);
    }

    async createContactAsync(model){
        return await postAsync(endpoint, model);
    }

    async updateContactAsync(model){
        return await putAsync(`${endpoint}/${model.contactId}`, model)
    }

    async deleteContactAsync(userId, friendId) {
        return await deleteAsync(`${endpoint}/${userId}/${friendId}`);
    }
}

export default new ContactApiService()