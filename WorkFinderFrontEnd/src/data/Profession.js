class Profession {
    constructor(jsonObject) {
        this.Id = jsonObject.Id;
        this.Name = jsonObject.Name;
        this.IdProfessionCategory = jsonObject.IdProfessionCategory;
    }
}

export default Profession;