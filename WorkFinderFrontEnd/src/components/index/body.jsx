import React from 'react';
import IndexCategoryBox from "./IndexCategoryBox.jsx"
import styles from "../../../styles/styles.scss";
import apiGetRequest from "../../tools/apiGetRequest.js"
import Promise from 'promise-polyfill';

class PageBody extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            professionCategories: []
        };

        this.updateProfessionCategory = this.updateProfessionCategory.bind(this);
    }

    componentWillMount() {
        this.updateProfessionCategory();
    }

    updateProfessionCategory(){
        const professionCategoryPromise = apiGetRequest("api/v1/ProfessionCategory");

        professionCategoryPromise.then((promise) => {
            this.setState({professionCategories: promise.data});
          });
    }
   
    render() {
        console.log(this.state.professionCategories);
        return(
                <div>
                    <div className="search">
                        <div className="search-image">
                            <div className="search-panel border">
                                <p className="search-title">Выбирайте проверенных специалистов</p>
                                <input type="text" className="border" />
                                <input type="text" className="border" />
                                <button className="border">Начать подбор</button>
                                <p>Например, <a href="Index.html">устройство полов</a> </p> 
                            </div>
                        </div>
                    </div>
                    <div class="catalog">
                        <div className="catalog-tables">
                        { 
                            this.state.professionCategories.length != 0 && 
                            this.state.professionCategories.map(professionCategory => {
                                return <IndexCategoryBox name={professionCategory.Name} id={professionCategory.Id}/>;
                            })
                        }
                        </div>
                    </div>
                </div>
        );
    }
    // api/v1/ProfessionCategory/5/Professions
    // api/v1/ProfessionCategory/
}

export default PageBody;