import styles from "../../../styles/styles.scss";
import apiGetRequest from "../../tools/apiGetRequest";
import ReactSelect from 'react-select';

import React from 'react';

class IndexApp extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            professions: [],
            professionIds: []
        };

        this.updateProfessions = this.updateProfessions.bind(this);
        this.onProfessionsChange = this.onProfessionsChange.bind(this);
        this.getProfessions = this.getProfessions.bind(this);
    }

    componentWillMount() {
        this.updateProfessions();
    }

    updateProfessions() {
        const requestPromise = apiGetRequest("/api/v1/profession");

        requestPromise.then(responseObject => {
            this.setState({
                professions: responseObject.data,
            });
        })
    }

    getProfessions() {
        let professions = [];

        this.state.professions.map((profession) => {
            professions.push({value: profession.Id, label: profession.Name})
        });

        return professions;
    }

    onProfessionsChange(professions) {
        let ids = [];

        professions.map((profession) => {
            ids.push(profession.value);
        });

        this.setState({
            professionIds: ids
        });

        console.log(this.state.professionIds);
    }

    render() {
        const professionOptions = this.getProfessions();
        
        return(
            <div>
                <h1>Basic api request example</h1>
                <div className="select-wrapper">
                <ReactSelect
                    options={professionOptions}
                    isClearable={true}
                    isSearchable={true}
                    isDisabled={false}
                    isMulti={true}
                    className="multi-select"
                    classNamePrefix="select"
                    onChange={this.onProfessionsChange}
                    placeholder={"Select professions..."}
                    blurInputOnSelect={false}
                    closeMenuOnSelect={false}
                    maxMenuHeight={215}
                />
                </div>
                <div>
                    <form asp-action="AddFile" asp-controller="Home" method="post" enctype="multipart/form-data">
                        <input type="file" name="uploadedFile" /><br/>
                        <input type="submit" value="Загрузить" />
                    </form>
                </div>
            </div>
        );
    }
}

export default IndexApp;
