import React from 'react';
import styles from "../../../styles/styles.scss";
import apiGetRequest from "../../tools/apiGetRequest.js"
import Promise from 'promise-polyfill';

class IndexCategoryBox extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            professions: []
        };

        this.updateProfessions = this.updateProfessions.bind(this);
    }

    componentWillMount() {
        this.updateProfessions();
    }

    updateProfessions(){
        const professionsPromise = apiGetRequest("api/v1/ProfessionCategory/" + this.props.id + "/Professions");

        professionsPromise.then((promise) => {
            this.setState({professions: promise.data});
          });
    }
   
    render() {
        //console.log(this.props.names);
        return(
            <div className="catalog-flex" >
                <a href ="Index.html" className="catalog-item-flex catalog-head-link">{this.props.name}</a>
                {
                    this.state.professions.length != 0 && 
                        this.state.professions.map(profession => {
                            return <div><a href ="Index.html" className="catalog-item-flex">{profession.Name}</a></div>;
                        })
                }
            </div>
        );
    }
}

export default IndexCategoryBox;