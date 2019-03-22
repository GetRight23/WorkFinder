import styles from "../../../styles/styles.scss";
import apiGetRequest from "../../tools/apiGetRequest";

import React from 'react';
import Photo from "../../data/photo.js"

class PhotoApp extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            photo: null
        };

        this.updatePhoto = this.updatePhoto.bind(this);
    }

    componentWillMount() {
        this.updatePhoto();
    }

    updatePhoto() {
        const requestPromise = apiGetRequest("/api/v1/photo/1");

        requestPromise.then(responseObject => {
            this.setState({
                photo: new Photo(responseObject.data) 
            });
        })
    }
   
    render() {
        return(
            <div>
                {
                    this.state.photo && 
                    <img src={"data:image/png;base64, " + this.state.photo.Data} alt="photo test" width = '400' height = '400' />
                }         
            </div>       
        );
    }
}

export default PhotoApp;


