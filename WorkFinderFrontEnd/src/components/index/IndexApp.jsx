import styles from "../../../styles/styles.scss";
import apiGetRequest from "../../tools/apiGetRequest";
import ReactSelect from 'react-select';

import Header from "../common/Header.jsx";
import React from 'react';
import PageBody from '../common/body.jsx'

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
        let tmp = [];

        this.state.professions.map((profession) => {
            tmp.push({value: profession.Id, label: profession.Name})
        });

        return tmp;
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
        const headNames = ['Home', 'About', 'Products', 'Contact'];       
        return(
            <div>
                 <Header names={headNames} />
                 <PageBody/>
            </div>                     
        );
    }
}

export default IndexApp;
