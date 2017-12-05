import { getAsync, postAsync, putAsync, deleteAsync, putFileAsync } from '../helpers/apiHelper';

const endpoint = "/api/user";

class UserApiServices {
    constructor() {

    }

    async getUserAsync(emailUser) {
        return await getAsync(`${endpoint}/${emailUser}`);
    }

    async updateUserAsync(model) {
        return await putAsync(`${endpoint}/${model.userId}`, model);
    }

    async getUserId(emailUser){
        return await getAsync(`${endpoint}/${emailUser}`);
    }

    async updateFileAnsync(data, userId) {
        console.log("UserApi");
        return await putFileAsync(`${endpoint}/img/${userId}`, data);
    }
}

export default new UserApiServices();