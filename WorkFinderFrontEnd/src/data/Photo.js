class Photo {
    constructor(jsonObject) {
        this.Id = jsonObject.Id;
        this.Data = jsonObject.Data;
        this.IdUser = jsonObject.IdUser;
    }
}

export default Photo;