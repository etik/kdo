import { putFileAsync } from '../helpers/apiHelper';

const endpoint = "/api/file";

class FileApiService {
    constructor() {

    }

    async updateFileAsync(data, userId, parameters) {
        console.log("updateFileAsync");
        console.log("tg ; " + parameters);
        return await putFileAsync(`${endpoint}/img/${userId}`, data, parameters);
    }

}

export default new FileApiService();