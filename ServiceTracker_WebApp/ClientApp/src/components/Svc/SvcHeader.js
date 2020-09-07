import React, { Component } from 'react';
import 'react-dates/initialize';
import 'react-dates/lib/css/_datepicker.css';
import { DateRangePicker } from 'react-dates';
import cookie from 'react-cookies';
import moment from 'moment';
import '@fortawesome/fontawesome-free/css/all.min.css'
class Header extends Component {
    constructor(props) {
        super(props);
        var selectedSdl = cookie.load('selectedSdl');
        var start = cookie.load('startDate');
        var end = cookie.load('endDate');
        if (start !== undefined && end !== undefined && start !== null && end !== null) {
            start = moment(start, 'DD-MM-YYYY');
            end = moment(end, 'DD-MM-YYYY');
        }
        else {
            start = end = null;
        }

        this.state = {
            error: null,
            data: null,
            focusedInput: null,
            startDate: start,
            endDate: end,
            selectedSdl: selectedSdl
        };
        
    }


    componentDidMount(e) {
        if (this.state.startDate !== undefined &&
            this.state.startDate  !== null &&
            this.state.endDate !== undefined &&
            this.state.endDate !== null &&
            this.state.selectedSdl !== undefined &&
            this.state.selectedSdl  !== null) {
            //var ev = { target: this.refs.btnSearch };
            //this.handleClick(ev);
        }
    }


    componentDidUpdate(prevProps, prevState) {



    }

    handleClick(e) {
        if (e.target === this.refs.btnNew) {
            this.props.newSvcCalBack(e);
            return;
        }
        if (e.target === this.refs.btnDuplicate) {
            this.props.duplicateSvcCalBack(e);
            return;
        }
        if (e.target === this.refs.btnDelete) {
            this.props.deleteSvcCalBack(e);
            return;
        }
        if (e.target === this.refs.btnBlock) {
            this.props.blockSvcCalBack(e);
            return;
        }
        if (e.target === this.refs.btnUnblock) {
            this.props.unblockSvcCalBack(e);
            return;
        }
        if (e.target === this.refs.btnSearch) {
            let s = this.refs.sdlSelection;
            let sdl = s.options[s.selectedIndex].text;
            if (s.selectedIndex > 0 &&
                this.state.startDate != null
                && this.state.endDate != null) {
                this.props.getSvcDataCalBack(
                    this,
                    sdl,
                    this.state.startDate.format('DD-MM-YYYY'),
                    this.state.endDate.format('DD-MM-YYYY')
                );
            }
            return;
        }
        if (e.target === this.refs.btnCommit) {
            let s = this.refs.sdlSelection;
            let sdl = s.options[s.selectedIndex].text;
            if (s.selectedIndex > 0 &&
                this.state.startDate != null
                && this.state.endDate != null) {
                this.props.commitSvcCalBack(
                    this,
                    sdl,
                    this.state.startDate.format('DD-MM-YYYY'),
                    this.state.endDate.format('DD-MM-YYYY')
                );
            }
            return;
        }
    }

    isOutsideRange(e) {
        return false;
    }

    onDatesChange(dateRange) {
        let from = dateRange.startDate;
        let to = dateRange.endDate;
        if (from !== null) {
            this.setState({ startDate: dateRange.startDate, })
            cookie.save('startDate', dateRange.startDate.format('DD-MM-YYYY'));
        }
        if (to !== null) {
            this.setState({ endDate: dateRange.endDate })
            cookie.save('endDate', dateRange.endDate.format('DD-MM-YYYY'));
        }
    }

    sdlChanged(e) {
        cookie.save('selectedSdl', e.target.value);
        this.setState({ selectedSdl: e.target.value })
    }

    render() {
        //if (this.props.sdl != null && this.state.selectedSdl !== null && this.state.selectedSdl !== undefined) {
        //    this.refs.sdlSelection.value = this.state.selectedSdl;
        //}

        let sdls = this.props.sdlCatalog == null ?
            null :
            this.props.sdlCatalog.map(s =>
                <option key={s.id} value={s.id}>{s.name}</option>
            );
        return (
                <div className="row">
                    <div className="col-md-2">
                        <h4>Service Tracker</h4>
                    </div>
                    <div className="col-md-10">
                        <form className="form-inline">
                            <div className="mb-2">
                                <label htmlFor="SDL" className="sr-only">SDL</label>
                                <select
                                    ref='sdlSelection'
                                    onChange={this.sdlChanged.bind(this)}
                                    className="form-control"
                                    placeholder="Select SDL"
                                    value={this.state.selectedSdl}>
                                    <option defaultValue>Choose...</option>
                                    {sdls}
                                </select>
                            </div>
                            <div className="form-group mx-sm-3 mb-2">
                                <DateRangePicker
                                    startDate={this.state.startDate} // momentPropTypes.momentObj or null,
                                    startDateId="fromDate" // PropTypes.string.isRequired,
                                    endDate={this.state.endDate} // momentPropTypes.momentObj or null,
                                    focusedInput={this.state.focusedInput} // PropTypes.oneOf([START_DATE, END_DATE]) or null,
                                    onFocusChange={focusedInput => this.setState({ focusedInput })} // PropTypes.func.isRequired,
                                    endDateId="toDate" // PropTypes.string.isRequired,
                                    enableOutsideDays={true}
                                    onDatesChange={this.onDatesChange.bind(this)} // PropTypes.func.isRequired,
                                    isOutsideRange={this.isOutsideRange.bind(this)}
                                    displayFormat='YYYY-MMM'
                                />
                            </div>
                            <div>
                                <button
                                    ref='btnSearch'
                                    type="button"
                                    onClick={this.handleClick.bind(this)}
                                    className="btn btn-dark mb-2 fas fa-search" title="Search" />
                                <button ref='btnNew' type="button" className="btn btn-dark ml-1 mb-2 fas fa-plus" onClick={this.handleClick.bind(this)} title="new" />
                                <button ref='btnDuplicate' type="button" className="btn btn-dark ml-1 mb-2 fas fa-copy" onClick={this.handleClick.bind(this)} title="Duplicate Selection" />
                                <button ref='btnDelete' type="button" className="btn btn-danger ml-1 mb-2 fas fa-minus" onClick={this.handleClick.bind(this)} title="Delete Selection" />
                                <button ref='btnCommit' type="button" className="btn btn-success ml-1 mb-2 fas fa-save" onClick={this.handleClick.bind(this)} title="Commit" />
                                <button ref='btnBlock' type="button" className="btn btn-dark ml-1 mb-2 fas fa-lock" onClick={this.handleClick.bind(this)} title="Lock" />
                                <button ref='btnUnblock' type="button" className="btn btn-dark ml-1 mb-2 fas fa-unlock" onClick={this.handleClick.bind(this)} title="Unlock" />
                            </div>
                        </form>
                    </div>
                </div>
        );
    }
}
export default Header;
