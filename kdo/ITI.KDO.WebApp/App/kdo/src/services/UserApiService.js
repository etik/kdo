import { getAsync, postAsync, putAsync, deleteAsync } from '../helpers/apiHelper';

const endpoint = "/api/user";

class UserApiServices {
    constructor() {

    }

    async getUserAsync(emailUser) {
        return await getAsync(`${endpoint}/${emailUser}`);
    }

    async getUserById(userId) {
        return await getAsync(`${endpoint}/${userId}/getById`);
    }

    async updateUserAsync(model) {
        return await putAsync(`${endpoint}/${model.userId}`, model);
    }
}

export default new UserApiServices();