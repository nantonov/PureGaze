import { DataGrid, type GridColDef } from "@mui/x-data-grid";

type SimpleTableProps = {
    rows: any[];
    columns: GridColDef<any>[];
    loading: boolean;
}

export function SimpleTable(props : SimpleTableProps){
    return(
        <DataGrid
            rows={props.rows}
            columns={props.columns}
            loading={props.loading}
            hideFooter={true}
        />
    );
}