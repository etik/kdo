import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/contact"

class ContactApiService{
    constructor(){

    }
    
    async getContactListAsync(userId){
        return await getAsync(`${endpoint}/${userId}/getAll`);
    }

    async getContactAsync(contactId){
        return await getAsync(`${endpoint}/${contactId}`);
    }

    async setContactInvitation(model){
        return await postAsync(`${endpoint}/setInvitation`, model);
    }

    async createContactAsync(model){
        return await postAsync(`${endpoint}/createContact`, model);
    }

    async deleteContactAsync(contactId){
        return await deleteAsync(`${endpoint}/${contactId}`);
    }
}

export default new ContactApiService()