import React from 'react';
import styles from "../../../styles/styles.scss";

class Header extends React.Component {

    constructor(props) {
        super(props);
    }

    componentWillMount() {
    }
   
    render() {
        console.log(this.props.names);
        return(
                <div className="header">
                    <ul className="header-navigation">
                        {
                            this.props.names.map(e => { 
                                    return <li><a href="#">{e}</a></li>; 
                                }
                            )
                        }
                    </ul>
                </div>
        );
    }
}

export default Header;
