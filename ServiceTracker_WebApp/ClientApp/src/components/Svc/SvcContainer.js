import React, { Component } from 'react';
import Svc from './Svc'
import LoadingOverlay from 'react-loading-overlay';
import Header from './SvcHeader'
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
//import './svcContainer.css'


export class SvcContainer extends Component {
    notify = (message, messageType) => {
        switch (messageType) {
            case 'info':
                toast.info(message, {});
                break;
            case 'success':
                toast.success(message, {});
                break;
            case 'warning':
                toast.warn(message, {});
                break;
            case 'error':
                toast.error(message, {});
                break;
            default:
                toast(message, {});
                break;
        }
    };
    constructor(props) {
        super(props);
        toast.configure({
            autoClose: 2000,
            pauseOnHover:false
        });
        this.state =
            {
                working: false,
                loading: true,
                clientsCatalog: null,
                blsCatalog: null,
                sdlCatalog: null,
                amCatalog: null,
                portfolioCatalog: null,
                subportfolioCatalog: null,
                costTypesCatalog: null,
                countriesCatalog: null,
                currenciesCatalog: null,
                employeesCatalog: null,
                selectedSdl: null,
                svcs: null
            };
    }

    componentDidMount() {
        fetch('api/service/GetCatalogs/')
            .then(response => response.json())
            .then(data => {
                this.setState({
                    loading: false,
                    clientsCatalog: data.clients,
                    blsCatalog: data.bls,
                    sdlCatalog: data.sdls,
                    amCatalog: data.ams,
                    costTypesCatalog: data.costTypes,
                    currenciesCatalog: data.currencies,
                    employeesCatalog: data.employees,
                    countriesCatalog: data.countries,
                    portfolioCatalog: data.portfolios,
                    subportfolioCatalog: data.subportfolios
                })
            });
    }

    callBackSvc(e, sdl, fromDate, toDate) {
        this.setState({ working: true });
        fetch(`api/service/GetData?sdl=${sdl}&from=${fromDate}&to=${toDate}`)
            .then(response => response.json())
            .then(d => {
                this.setState({ svcs: d, working: false })
            });
    }

    callBackNewSvc(e) {
        this.refs.childSvc.newSvc();
    }
    callBackDuplicateSvc(e) {
        this.refs.childSvc.duplicateSvc();
    }

    callBackDeleteSvc(e) {
        this.refs.childSvc.deleteSvc();
    }

    callBackBlockSvc(e) {
        this.refs.childSvc.lockSvc(true);
    }
    callBackUnblockSvc(e) {
        this.refs.childSvc.lockSvc(false);
    }


    callBackCommitSvc(e, sdl, fromDate, toDate) {
        if (this.state.svcs !== null) {
            this.refs.childSvc.refs.svcTable.hotInstance.validateCells(
                (valid) => {
                    if (valid) {
                        var svcs = this.state.svcs.filter(function (svc) {
                            return svc.status !== "UNCHANGED";
                        });
                        if (svcs.length > 0) {
                            //this.notify('Saving data...', 'success');
                            this.setState({ working: true });
                            fetch(`api/service/Commit?sdl=${sdl}&from=${fromDate}&to=${toDate}`, {
                                method: 'PUT',
                                body: JSON.stringify(svcs),
                                headers: {
                                    "Content-type": "application/json; charset=UTF-8"
                                }
                            })
                                .then(response => response.json())
                                .then(d => this.setState({ svcs: d, working: false }));
                        }
                    }
                    else {
                        this.notify('Make sure data is valid before saving.', 'error');
                    }
                });
        }
    }


    render() {
        if (this.state.loading) {
            return (
                <div className="center-div">
                    <div className="spinner-grow" role="status">
                        <span className="sr-only">Loading...</span>
                    </div>
                </div>
            );
        }
        else {
            return (
                <LoadingOverlay
                    active={this.state.working}
                    spinner
                    text='Working...'
                >

                    <Header
                        sdlCatalog={this.state.sdlCatalog}
                        getSvcDataCalBack={this.callBackSvc.bind(this)}
                        newSvcCalBack={this.callBackNewSvc.bind(this)}
                        duplicateSvcCalBack={this.callBackDuplicateSvc.bind(this)}
                        deleteSvcCalBack={this.callBackDeleteSvc.bind(this)}
                        commitSvcCalBack={this.callBackCommitSvc.bind(this)}
                        blockSvcCalBack={this.callBackBlockSvc.bind(this)}
                        unblockSvcCalBack={this.callBackUnblockSvc.bind(this)}
                    />
                    <Svc ref="childSvc"
                        clientsCatalog={this.state.clientsCatalog}
                        blsCatalog={this.state.blsCatalog}
                        sdlCatalog={this.state.sdlCatalog}
                        amCatalog={this.state.amCatalog}
                        portfolioCatalog={this.state.portfolioCatalog}
                        subportfolioCatalog={this.state.subportfolioCatalog}
                        costTypesCatalog={this.state.costTypesCatalog}
                        countriesCatalog={this.state.countriesCatalog}
                        currenciesCatalog={this.state.currenciesCatalog}
                        employeesCatalog={this.state.employeesCatalog}
                        selectedSdl={this.state.selectedSdl}
                        svcs={this.state.svcs}
                    />
                </LoadingOverlay>
            );

        }
    }
}