import { React } from 'react';

function TotalItem(props) {
    return
    (
        <div>
            <span>{props.key}:</span><span>props.value</span>
        </div>
    );
}

export default TotalItem;
