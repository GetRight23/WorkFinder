import React from 'react';
import styles from "../../../styles/styles.scss";

class PageBody extends React.Component {

    constructor(props) {
        super(props);
    }

    componentWillMount() {
    }
   
    render() {
        console.log(this.props.names);
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
                </div>
        );
    }
}

export default PageBody;