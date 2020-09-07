import React, { Fragment, Component } from 'react';
import 'handsontable/dist/handsontable.full.css';
import { HotTable, } from '@handsontable/react';
import Handsontable from 'handsontable'

class Svc extends Component {
    static displayName = Svc.name;

    constructor(props) {
        super(props);
        this.rowOffset = 0;
        this.canResizeTable = false;
        this.state = {
            totals: [],
            tblInitialized: false,
            error: null,
            sdl: [],
            focusDatePickerInput: false,
            hiddenColumns: {
                columns: [0, 1, 2, 3, 4],
                indicators: false
            },
            columns: [
                { data: 'id', type: 'text' },
                { data: 'status', type: 'text' },
                { data: 'blocked', type: 'text' },
                { data: 'domainId', type: 'text' },
                { data: 'accountId', type: 'text' },
                { data: 'country', type: 'dropdown', source: this.props.countriesCatalog.map(s => s.name)},
                { data: 'sdl', type: 'dropdown', source: this.props.sdlCatalog.map(s => s.name)},
                { data: 'am', type: 'dropdown', source: this.props.amCatalog.map(s => s.name) },
                { data: 'strDate', type: 'date', dateFormat: 'YYYY-MMM' },
                { data: 'quoteFtl', type: 'text'},
                { data: 'po', type: 'text' },
                { data: 'client', type: 'dropdown', source: this.props.clientsCatalog.map(s => s.name) },
                { data: 'field', type: 'text' },
                { data: 'well', type: 'text' },
                //{ data: 'au', type: 'text' },
                //{ data: 'ac', type: 'text' },
                { data: 'portfolio', type: 'dropdown', source: this.props.portfolioCatalog.map(s => s.name) },
                { data: 'subPortfolio', type: 'dropdown', source: this.props.subportfolioCatalog.map(s => s.name) },
                { data: 'masterCode', type: 'text' },
                { data: 'currency', type: 'dropdown', source: this.props.currenciesCatalog.map(s => s.name) },
                { data: 'fxrate', type: 'text' },
                { data: 'comment', type: 'text' },
                { data: 'technicalLead', type: 'dropdown', source: this.props.employeesCatalog.map(s => s.name) },
                { data: 'changePointTask', type: 'text' },
                { data: 'rofo', type: 'numeric', numericFormat: { pattern: '0,0.00 $' }, width: 120 },
                { data: 'iMf', type: 'numeric', numericFormat: { pattern: '0,0.00 $' }, width: 120 },
                { data: 'mmf', type: 'numeric', numericFormat: { pattern: '0,0.00 $' }, width: 120 },
                { data: 'sentToInvoice', type: 'numeric', numericFormat: { pattern: '0,0.00 $' }, width: 120 },
                { data: 'revenue', type: 'numeric', numericFormat: { pattern: '0,0.00 $' }, width: 120 },
                { data: 'invocieNumber', type: 'text' },
                { data: 'cost', type: 'numeric', numericFormat: { pattern: '0,0.00 $' }, width: 120 },
                { data: 'costReceived', type: 'numeric', numericFormat: { pattern: '0,0.00 $' }, width: 120 },
                { data: 'costType', type: 'dropdown', source: this.props.costTypesCatalog.map(s => s.name) },
                //{ data: 'glaccount', type: 'text' },
                { data: 'costDescription', type: 'text' }
            ],
            colHeaders: [
                'Id',
                'Status',
                'Blocked',
                'DomainId',
                'AccountId',
                'Country',
                'SDL',
                'AM',
                'Date',
                'QuoteFtl',
                'Po',
                'Client',
                'Field',
                'Well',
                //'Au',
                //'Ac',
                'Portfolio',
                'SubPortfolio',
                'MasterCode',
                'Currency',
                'Fxrate',
                'Comment',
                'TechnicalLead',
                'ChangePointTask',
                'Rofo',
                'IMf',
                'Mmf',
                'SentToInvoice',
                'Revenue',
                'InvocieNumber',
                'Cost',
                'CostReceived',
                'CostType',
                'CostDescription'
            ],
            columnSorting: {
                sortEmptyCells: true,
                indicator: true, // true = shows indicator for all columns, false = don't show indicator for columns
                headerAction: true,
                compareFunctionFactory:
                    function (sortOrder, columnMeta) {
                        return function (value, nextValue) {
                            if (value < nextValue) {
                                return 1;
                            }
                            if (value === nextValue) {
                                return 0;
                            }
                            if (value > nextValue) {
                                return -1;
                            }
                            return 0;
                        }
                    }
            }
        }
    }

    componentDidMount() {
        this.setSummaries();
    }
    componentDidUpdate(prevProps, prevState, snapshot) {
        if (prevProps.svcs !== this.props.svcs && this.refs.svcTable !== undefined) {
            const filtersPlugin = this.refs.svcTable.hotInstance.getPlugin('filters');
            for (var i = 0; i < this.refs.svcTable.hotInstance.countCols(); i++) {
                filtersPlugin.removeConditions(i);
            }
            filtersPlugin.filter();
            this.calculateSummaries();
        }
    }

    newSvc() {
        this.refs.svcTable.hotInstance.alter("insert_row", this.rowOffset);
        this.refs.svcTable.hotInstance.setDataAtCell(this.rowOffset, 2, 0);
        this.refs.svcTable.hotInstance.setDataAtCell(this.rowOffset, 0, -1);
        this.refs.svcTable.hotInstance.setDataAtCell(this.rowOffset, 1, 'ADDED');

    }

    setSummaries() {
        if (this.refs.svcTable !== undefined) {
            var colCount = this.refs.svcTable.hotInstance.countCols();
            var rowCount = this.refs.svcTable.hotInstance.countRows();
            var t = [];
            for (var i = 0; i < colCount; i++) {
                if (this.state.columns[i].type === 'numeric') {
                    var aggregated = 0;
                    for (var j = 0; j < rowCount; j++) {
                        var valueToAggregate = this.refs.svcTable.hotInstance.getDataAtCell(j, i);
                        aggregated += valueToAggregate;
                    }
                    t.push({ colIdx: i, key: this.state.columns[i].data, value: aggregated });
                }
            };
            this.setState({ totals: t })
        }
    }

    calculateSummaries() {
        var rowCount = this.refs.svcTable.hotInstance.countRows();
        var t = this.state.totals;
        for (var i = 0; i < t.length; i++) {
            var aggregated = 0;
            for (var j = 0; j < rowCount; j++) {
                var valueToAggregate = this.refs.svcTable.hotInstance.getDataAtCell(j, t[i].colIdx);
                aggregated += valueToAggregate;
            }
            t[i].value = aggregated;
        };
        this.setState({ totals: t })
    }


    updateSummaries(oldValue, newValue, col) {
        if (col === undefined) {
            return;
        }
        if (isNaN(col)) {
            for (let i = 0; i < this.state.columns.length; i++) {
                if (this.state.columns[i].data === col) {
                    col = i;
                    break;
                }
            }
        }
        if (this.state.columns[col].type === 'numeric') {
            oldValue = parseFloat(oldValue);
            newValue = parseFloat(newValue);
            if (isNaN(oldValue)) {
                oldValue = 0;
            }
            if (isNaN(newValue)) {
                newValue = 0;
            }
            var t = this.state.totals;
            for (let i = 0; i < t.length; i++) {
                if (t[i].colIdx === col) {
                    t[i].value = t[i].value - oldValue + newValue;
                }
            }
            this.setState({totals: t});
        }
    }

    duplicateSvc() {
        var selection = this.refs.svcTable.hotInstance.getSelected();
        if (selection !== undefined) {
            var startRow = selection[0][0];
            var endRow = selection[0][2];
            if (startRow > endRow) {
                startRow = selection[0][2];
                endRow = selection[0][0];
            }
            this.refs.svcTable.hotInstance.selectRows(startRow, endRow);
            var colCount = this.refs.svcTable.hotInstance.countCols();
            let changes = []
            for (var rowIdx = startRow, rootIdx = this.rowOffset; rowIdx <= endRow; rowIdx++ , rootIdx++) {
                var status = this.refs.svcTable.hotInstance.getDataAtCell(rowIdx, 1);
                changes.push([rootIdx, 2, false]);
                if (status === "DELETED") {
                    continue;
                }
                for (var colIdx = 3; colIdx < colCount; colIdx++) {
                    let cellVal = this.refs.svcTable.hotInstance.getDataAtCell(rowIdx + rootIdx, colIdx);
                    if (cellVal != null) {
                        changes.push([rootIdx, colIdx, cellVal]);
                    }
                }
                this.newSvc();
            }
            this.refs.svcTable.hotInstance.setDataAtCell(changes);
            this.calculateSummaries();
        }
    }

    deleteSvc() {
        var selection = this.refs.svcTable.hotInstance.getSelected();
        if (selection !== undefined) {
            var startRow = selection[0][0];
            var endRow = selection[0][2];
            if (startRow > endRow) {
                startRow = selection[0][2];
                endRow = selection[0][0];
            }
            this.refs.svcTable.hotInstance.selectRows(startRow, endRow);
            let changes = []
            for (var rowIdx = startRow; rowIdx <= endRow; rowIdx++) {
                var status = this.refs.svcTable.hotInstance.getDataAtCell(rowIdx, 1);
                var locked = this.refs.svcTable.hotInstance.getDataAtCell(rowIdx, 2);
                if (!locked) {
                    if (status !== "ADDED") {
                        changes.push([rowIdx, 1, "DELETED"]);
                    }
                }
            }
            this.refs.svcTable.hotInstance.setDataAtCell(changes);
        }
    }


    lockSvc(locked) {
        var selection = this.refs.svcTable.hotInstance.getSelected();
        if (selection !== undefined) {
            var startRow = selection[0][0];
            var endRow = selection[0][2];
            if (startRow > endRow) {
                startRow = selection[0][2];
                endRow = selection[0][0];
            }
            this.refs.svcTable.hotInstance.selectRows(startRow, endRow);
            let changes = []
            for (var rowIdx = startRow; rowIdx <= endRow; rowIdx++) {
                var status = this.refs.svcTable.hotInstance.getDataAtCell(rowIdx, 1);
                if (status !== "ADDED") {
                    changes.push([rowIdx, 2, locked]);
                    changes.push([rowIdx, 1, "UPDATED"]);
                }
            }
            this.refs.svcTable.hotInstance.setDataAtCell(changes);
        }
    }


    cellSettings(row, col, prop) {
        var cellProperties = { className: '' };
        if (this.refs.svcTable !== undefined) {
            var locked = this.refs.svcTable.hotInstance.getDataAtCell(row, 2);
            var colType = this.state.columns[col].type;
            var status = this.refs.svcTable.hotInstance.getDataAtCell(row, 1);
            var cellReadOnly = false;
            var fontWeight = '';
            var color = '';
            var background = '';
            if (locked) {
                cellReadOnly = true;
                fontWeight = '';
                color = '#6F6F6F';
                background = '#F4F4F4';
            }
            else {
                switch (status) {
                    case this.props.summaryRowType:
                        cellReadOnly = true;
                        fontWeight = 'bold';
                        background = '#F9FAD4';
                        color = colType === 'numeric' ? 'Black' : background;
                        break;
                    case 'DELETED':
                        cellReadOnly = true;
                        fontWeight = '';
                        color = '#802900';
                        background = '#ffa7a7';
                        break;
                    case 'UPDATED':
                        cellReadOnly = false;
                        fontWeight = '';
                        color = '#0B0080';
                        background = '#E2F1F7';
                        break;
                    case 'ADDED':
                        cellReadOnly = false;
                        fontWeight = '';
                        color = '#00803F';
                        background = '#C7FFB8';
                        break;
                    case 'UNCHANGED':
                    default:
                        cellReadOnly = false;
                        fontWeight = '';
                        color = 'Black';
                        background = '#FDFFFC';
                        break;
                }
            }
            cellProperties.renderer = function (instance, td, row, col, prop, value, cellProperties) {
                cellProperties.readOnly = cellReadOnly;
                td.style.fontWeight = fontWeight;
                td.style.color = color;
                td.style.background = background;
                switch (colType) {
                    case "dropdown":
                        Handsontable.renderers.DropdownRenderer.apply(this, arguments);
                        break;
                    case "text":
                        Handsontable.renderers.TextRenderer.apply(this, arguments);
                        break;
                    case "numeric":
                        Handsontable.renderers.NumericRenderer.apply(this, arguments);
                        break;
                    default:
                        Handsontable.renderers.TextRenderer.apply(this, arguments);
                        break;
                }
            };
            return cellProperties;
        }
    }

    afterColumnSort() {
        //var sumRow = this.getSummaryRow();
        //const plugin = this.refs.svcTable.hotInstance.getPlugin('manualRowMove');
        //plugin.moveRow(sumRow, 0);
        //plugin.enablePlugin();
        //this.refs.svcTable.hotInstance.render();
    }


    afterCellChanged(e) {
        if (e !== null && e.length > 0) {
            var row = e[0][0];
            var col = e[0][1];
            var oldVal = e[0][2];
            var newVal = e[0][3];
            if (row !== null && col !== null && col !== 'status') {
                var statusVal = this.refs.svcTable.hotInstance.getDataAtCell(row, 1);
                if (statusVal === 'UNCHANGED') {
                    this.refs.svcTable.hotInstance.setDataAtCell(row, 1, 'UPDATED');
                }
                this.updateSummaries(oldVal, newVal, col);
            }
        }
    }


    //componentWillUnmount() {
    //    //clearTimeout(this.timeoutId);

    //}

    rowCreated(index, amount, source) {
    }


    afterTableInit() {


    }

    beforeFilter(conditions) {
    }

    afterFilter(conditions) {
        this.calculateSummaries();
    }

    beforeValidate(value, row, prop, source) {
    }

    afterValidate(isValid, value, row, prop, source) {
        switch (prop) {
            case "sdl":
            case "am":
            case "country":
            case "strDate":
                return value !== null && value !== '';
            default:
                return true;
        }
    }

    afterLoadData(initialLoad) {


    }

    activateResizeTable(e) {
        this.canResizeTable = true;
    }

    deactivateResizeTable(e) {
        this.canResizeTable = false;
    }

    resizeTable(e) {
        if (this.canResizeTable) {
            e = e || window.event;
            e.preventDefault();
            this.refs.svcTable.hotInstance.updateSettings({ height: e.clientY });

            this.refs.resizeBar.style.top = e.clientY;

            //var tableHeight = this.refs.resizeBar.style
            //this.refs.svcTable.hotInstance.height = e.ClientY -  
            //elmnt.style.top = (elmnt.offsetTop - pos2) + "px";

        }
    }


    render() {
        if (this.state.error) {
            return `Error ${this.state.error.message}`;
        }
        if (
            this.props.svcs === null ||
            this.props.svcs.length > 0
        ) {
            return (
                <div ref="container">
                    <div>
                        {
                            this.state.totals.map(function (t) {
                                return (
                                    <Fragment key={'f' + t.colIdx}>
                                        <span key={'k' + t.colIdx} className="pr-3 summaryKey">{t.key}:</span>
                                        <span key={'v' + t.colIdx} className="pr-3 summaryValue">${(t.value).toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,')}</span>
                                    </Fragment>
                                );
                            })
                        }
                    </div>
                    <HotTable
                        afterInit={this.afterTableInit.bind(this)}
                        afterCreateRow={this.rowCreated.bind(this)}
                        afterLoadData={this.afterLoadData.bind(this)}
                        allowInsertColumn={false}
                        allowInsertRow={false}
                        allowRemoveColumn={false}
                        allowRemoveRow={false}
                        afterFilter={this.afterFilter.bind(this)}
                        beforeFilter={this.beforeFilter.bind(this)}
                        beforeValidate={this.beforeValidate.bind(this)}
                        afterValidate={this.afterValidate.bind(this)}
                        columnSorting={false}
                        dropdownMenu={['filter_by_condition', 'filter_operators', 'filter_by_condition2', 'filter_by_value' , 'filter_action_bar']}
                        contextMenu={false}
                        ref="svcTable"
                        root="svcTable"
                        data={this.props.svcs}
                        //stretchH='all'
                        cells={this.cellSettings.bind(this)}
                        columns={this.state.columns}
                        hiddenColumns={this.state.hiddenColumns}
                        colHeaders={this.state.colHeaders}
                        afterSetDataAtCell={this.afterCellChanged.bind(this)}
                        afterColumnSort={this.afterColumnSort.bind(this)}
                        persistentState={true}
                        //columnSummary={this.setSummaryRow.bind(this)}
                        //autoWrapRow={true}
                        //width='100%'
                        height={window.innerHeight -80}
                        overflow='hidden'
                        maxRows='100'
                        manualRowResize={true}
                        manualColumnResize={true}
                        rowHeaders={true}
                        manualRowMove={false}
                        manualColumnMove={false}
                        filters={true}
                        undo={false}
                        readOnly={false}
                        outsideClickDeselects={false}
                        licenseKey='non-commercial-and-evaluation'
                    />
                </div>

            );
        }
        else {
            return <div className="content">No Data Available</div >;
        }

    }
}
export default Svc;