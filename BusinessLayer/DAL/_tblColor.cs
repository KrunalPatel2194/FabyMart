
/*
'===============================================================================
'  Generated From - CSharp_dOOdads_BusinessEntity.vbgen
' 
'  ** IMPORTANT  ** 
'  How to Generate your stored procedures:
' 
'  SQL        = SQL_StoredProcs.vbgen
'  ACCESS     = Access_StoredProcs.vbgen
'  ORACLE     = Oracle_StoredProcs.vbgen
'  FIREBIRD   = FirebirdStoredProcs.vbgen
'  POSTGRESQL = PostgreSQL_StoredProcs.vbgen
'
'  The supporting base class SqlClientEntity is in the Architecture directory in "dOOdads".
'  
'  This object is 'abstract' which means you need to inherit from it to be able
'  to instantiate it.  This is very easilly done. You can override properties and
'  methods in your derived class, this allows you to regenerate this class at any
'  time and not worry about overwriting custom code. 
'
'  NEVER EDIT THIS FILE.
'
'  public class YourObject :  _YourObject
'  {
'
'  }
'
'===============================================================================
*/

// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;

using MyGeneration.dOOdads;

namespace BusinessLayer
{
	public abstract class _tblColor : SqlClientEntity
	{
		public _tblColor()
		{
			this.QuerySource = "tblColor";
			this.MappingName = "tblColor";

		}	

		//=================================================================
		//  public Overrides void AddNew()
		//=================================================================
		//
		//=================================================================
		public override void AddNew()
		{
			base.AddNew();
			
		}
		
		
		public override void FlushData()
		{
			this._whereClause = null;
			this._aggregateClause = null;
			base.FlushData();
		}
		
		//=================================================================
		//  	public Function LoadAll() As Boolean
		//=================================================================
		//  Loads all of the records in the database, and sets the currentRow to the first row
		//=================================================================
		public bool LoadAll() 
		{
			ListDictionary parameters = null;
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblColorLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppColorID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppColorID, AppColorID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblColorLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppColorID
			{
				get
				{
					return new SqlParameter("@AppColorID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppColorName
			{
				get
				{
					return new SqlParameter("@AppColorName", SqlDbType.VarChar, 50);
				}
			}
			
			public static SqlParameter AppColorCode
			{
				get
				{
					return new SqlParameter("@AppColorCode", SqlDbType.VarChar, 10);
				}
			}
			
			public static SqlParameter AppIsActive
			{
				get
				{
					return new SqlParameter("@AppIsActive", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter AppDisplayOrder
			{
				get
				{
					return new SqlParameter("@AppDisplayOrder", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppIsDefault
			{
				get
				{
					return new SqlParameter("@AppIsDefault", SqlDbType.Bit, 0);
				}
			}
			
			public static SqlParameter AppColorImage
			{
				get
				{
					return new SqlParameter("@AppColorImage", SqlDbType.VarChar, 250);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string AppColorID = "appColorID";
            public const string AppColorName = "appColorName";
            public const string AppColorCode = "appColorCode";
            public const string AppIsActive = "appIsActive";
            public const string AppDisplayOrder = "appDisplayOrder";
            public const string AppIsDefault = "appIsDefault";
            public const string AppColorImage = "appColorImage";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppColorID] = _tblColor.PropertyNames.AppColorID;
					ht[AppColorName] = _tblColor.PropertyNames.AppColorName;
					ht[AppColorCode] = _tblColor.PropertyNames.AppColorCode;
					ht[AppIsActive] = _tblColor.PropertyNames.AppIsActive;
					ht[AppDisplayOrder] = _tblColor.PropertyNames.AppDisplayOrder;
					ht[AppIsDefault] = _tblColor.PropertyNames.AppIsDefault;
					ht[AppColorImage] = _tblColor.PropertyNames.AppColorImage;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppColorID = "AppColorID";
            public const string AppColorName = "AppColorName";
            public const string AppColorCode = "AppColorCode";
            public const string AppIsActive = "AppIsActive";
            public const string AppDisplayOrder = "AppDisplayOrder";
            public const string AppIsDefault = "AppIsDefault";
            public const string AppColorImage = "AppColorImage";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppColorID] = _tblColor.ColumnNames.AppColorID;
					ht[AppColorName] = _tblColor.ColumnNames.AppColorName;
					ht[AppColorCode] = _tblColor.ColumnNames.AppColorCode;
					ht[AppIsActive] = _tblColor.ColumnNames.AppIsActive;
					ht[AppDisplayOrder] = _tblColor.ColumnNames.AppDisplayOrder;
					ht[AppIsDefault] = _tblColor.ColumnNames.AppIsDefault;
					ht[AppColorImage] = _tblColor.ColumnNames.AppColorImage;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppColorID = "s_AppColorID";
            public const string AppColorName = "s_AppColorName";
            public const string AppColorCode = "s_AppColorCode";
            public const string AppIsActive = "s_AppIsActive";
            public const string AppDisplayOrder = "s_AppDisplayOrder";
            public const string AppIsDefault = "s_AppIsDefault";
            public const string AppColorImage = "s_AppColorImage";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppColorID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppColorID);
			}
			set
	        {
				base.Setint(ColumnNames.AppColorID, value);
			}
		}

		public virtual string AppColorName
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppColorName);
			}
			set
	        {
				base.Setstring(ColumnNames.AppColorName, value);
			}
		}

		public virtual string AppColorCode
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppColorCode);
			}
			set
	        {
				base.Setstring(ColumnNames.AppColorCode, value);
			}
		}

		public virtual bool AppIsActive
	    {
			get
	        {
				return base.Getbool(ColumnNames.AppIsActive);
			}
			set
	        {
				base.Setbool(ColumnNames.AppIsActive, value);
			}
		}

		public virtual int AppDisplayOrder
	    {
			get
	        {
				return base.Getint(ColumnNames.AppDisplayOrder);
			}
			set
	        {
				base.Setint(ColumnNames.AppDisplayOrder, value);
			}
		}

		public virtual bool AppIsDefault
	    {
			get
	        {
				return base.Getbool(ColumnNames.AppIsDefault);
			}
			set
	        {
				base.Setbool(ColumnNames.AppIsDefault, value);
			}
		}

		public virtual string AppColorImage
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppColorImage);
			}
			set
	        {
				base.Setstring(ColumnNames.AppColorImage, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_AppColorID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppColorID) ? string.Empty : base.GetintAsString(ColumnNames.AppColorID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppColorID);
				else
					this.AppColorID = base.SetintAsString(ColumnNames.AppColorID, value);
			}
		}

		public virtual string s_AppColorName
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppColorName) ? string.Empty : base.GetstringAsString(ColumnNames.AppColorName);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppColorName);
				else
					this.AppColorName = base.SetstringAsString(ColumnNames.AppColorName, value);
			}
		}

		public virtual string s_AppColorCode
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppColorCode) ? string.Empty : base.GetstringAsString(ColumnNames.AppColorCode);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppColorCode);
				else
					this.AppColorCode = base.SetstringAsString(ColumnNames.AppColorCode, value);
			}
		}

		public virtual string s_AppIsActive
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppIsActive) ? string.Empty : base.GetboolAsString(ColumnNames.AppIsActive);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppIsActive);
				else
					this.AppIsActive = base.SetboolAsString(ColumnNames.AppIsActive, value);
			}
		}

		public virtual string s_AppDisplayOrder
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppDisplayOrder) ? string.Empty : base.GetintAsString(ColumnNames.AppDisplayOrder);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppDisplayOrder);
				else
					this.AppDisplayOrder = base.SetintAsString(ColumnNames.AppDisplayOrder, value);
			}
		}

		public virtual string s_AppIsDefault
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppIsDefault) ? string.Empty : base.GetboolAsString(ColumnNames.AppIsDefault);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppIsDefault);
				else
					this.AppIsDefault = base.SetboolAsString(ColumnNames.AppIsDefault, value);
			}
		}

		public virtual string s_AppColorImage
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppColorImage) ? string.Empty : base.GetstringAsString(ColumnNames.AppColorImage);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppColorImage);
				else
					this.AppColorImage = base.SetstringAsString(ColumnNames.AppColorImage, value);
			}
		}


		#endregion		
	
		#region Where Clause
		public class WhereClause
		{
			public WhereClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffWhereParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffWhereParameter(this);
					}

					return _tearOff;
				}
			}

			#region WhereParameter TearOff's
			public class TearOffWhereParameter
			{
				public TearOffWhereParameter(WhereClause clause)
				{
					this._clause = clause;
				}
				
				
				public WhereParameter AppColorID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppColorID, Parameters.AppColorID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppColorName
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppColorName, Parameters.AppColorName);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppColorCode
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppColorCode, Parameters.AppColorCode);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppIsActive
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppIsActive, Parameters.AppIsActive);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppDisplayOrder
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppDisplayOrder, Parameters.AppDisplayOrder);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppIsDefault
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppIsDefault, Parameters.AppIsDefault);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppColorImage
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppColorImage, Parameters.AppColorImage);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter AppColorID
		    {
				get
		        {
					if(_AppColorID_W == null)
	        	    {
						_AppColorID_W = TearOff.AppColorID;
					}
					return _AppColorID_W;
				}
			}

			public WhereParameter AppColorName
		    {
				get
		        {
					if(_AppColorName_W == null)
	        	    {
						_AppColorName_W = TearOff.AppColorName;
					}
					return _AppColorName_W;
				}
			}

			public WhereParameter AppColorCode
		    {
				get
		        {
					if(_AppColorCode_W == null)
	        	    {
						_AppColorCode_W = TearOff.AppColorCode;
					}
					return _AppColorCode_W;
				}
			}

			public WhereParameter AppIsActive
		    {
				get
		        {
					if(_AppIsActive_W == null)
	        	    {
						_AppIsActive_W = TearOff.AppIsActive;
					}
					return _AppIsActive_W;
				}
			}

			public WhereParameter AppDisplayOrder
		    {
				get
		        {
					if(_AppDisplayOrder_W == null)
	        	    {
						_AppDisplayOrder_W = TearOff.AppDisplayOrder;
					}
					return _AppDisplayOrder_W;
				}
			}

			public WhereParameter AppIsDefault
		    {
				get
		        {
					if(_AppIsDefault_W == null)
	        	    {
						_AppIsDefault_W = TearOff.AppIsDefault;
					}
					return _AppIsDefault_W;
				}
			}

			public WhereParameter AppColorImage
		    {
				get
		        {
					if(_AppColorImage_W == null)
	        	    {
						_AppColorImage_W = TearOff.AppColorImage;
					}
					return _AppColorImage_W;
				}
			}

			private WhereParameter _AppColorID_W = null;
			private WhereParameter _AppColorName_W = null;
			private WhereParameter _AppColorCode_W = null;
			private WhereParameter _AppIsActive_W = null;
			private WhereParameter _AppDisplayOrder_W = null;
			private WhereParameter _AppIsDefault_W = null;
			private WhereParameter _AppColorImage_W = null;

			public void WhereClauseReset()
			{
				_AppColorID_W = null;
				_AppColorName_W = null;
				_AppColorCode_W = null;
				_AppIsActive_W = null;
				_AppDisplayOrder_W = null;
				_AppIsDefault_W = null;
				_AppColorImage_W = null;

				this._entity.Query.FlushWhereParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffWhereParameter _tearOff;
			
		}
	
		public WhereClause Where
		{
			get
			{
				if(_whereClause == null)
				{
					_whereClause = new WhereClause(this);
				}
		
				return _whereClause;
			}
		}
		
		private WhereClause _whereClause = null;	
		#endregion
	
		#region Aggregate Clause
		public class AggregateClause
		{
			public AggregateClause(BusinessEntity entity)
			{
				this._entity = entity;
			}
			
			public TearOffAggregateParameter TearOff
			{
				get
				{
					if(_tearOff == null)
					{
						_tearOff = new TearOffAggregateParameter(this);
					}

					return _tearOff;
				}
			}

			#region AggregateParameter TearOff's
			public class TearOffAggregateParameter
			{
				public TearOffAggregateParameter(AggregateClause clause)
				{
					this._clause = clause;
				}
				
				
				public AggregateParameter AppColorID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppColorID, Parameters.AppColorID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppColorName
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppColorName, Parameters.AppColorName);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppColorCode
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppColorCode, Parameters.AppColorCode);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppIsActive
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppIsActive, Parameters.AppIsActive);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppDisplayOrder
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppDisplayOrder, Parameters.AppDisplayOrder);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppIsDefault
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppIsDefault, Parameters.AppIsDefault);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppColorImage
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppColorImage, Parameters.AppColorImage);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter AppColorID
		    {
				get
		        {
					if(_AppColorID_W == null)
	        	    {
						_AppColorID_W = TearOff.AppColorID;
					}
					return _AppColorID_W;
				}
			}

			public AggregateParameter AppColorName
		    {
				get
		        {
					if(_AppColorName_W == null)
	        	    {
						_AppColorName_W = TearOff.AppColorName;
					}
					return _AppColorName_W;
				}
			}

			public AggregateParameter AppColorCode
		    {
				get
		        {
					if(_AppColorCode_W == null)
	        	    {
						_AppColorCode_W = TearOff.AppColorCode;
					}
					return _AppColorCode_W;
				}
			}

			public AggregateParameter AppIsActive
		    {
				get
		        {
					if(_AppIsActive_W == null)
	        	    {
						_AppIsActive_W = TearOff.AppIsActive;
					}
					return _AppIsActive_W;
				}
			}

			public AggregateParameter AppDisplayOrder
		    {
				get
		        {
					if(_AppDisplayOrder_W == null)
	        	    {
						_AppDisplayOrder_W = TearOff.AppDisplayOrder;
					}
					return _AppDisplayOrder_W;
				}
			}

			public AggregateParameter AppIsDefault
		    {
				get
		        {
					if(_AppIsDefault_W == null)
	        	    {
						_AppIsDefault_W = TearOff.AppIsDefault;
					}
					return _AppIsDefault_W;
				}
			}

			public AggregateParameter AppColorImage
		    {
				get
		        {
					if(_AppColorImage_W == null)
	        	    {
						_AppColorImage_W = TearOff.AppColorImage;
					}
					return _AppColorImage_W;
				}
			}

			private AggregateParameter _AppColorID_W = null;
			private AggregateParameter _AppColorName_W = null;
			private AggregateParameter _AppColorCode_W = null;
			private AggregateParameter _AppIsActive_W = null;
			private AggregateParameter _AppDisplayOrder_W = null;
			private AggregateParameter _AppIsDefault_W = null;
			private AggregateParameter _AppColorImage_W = null;

			public void AggregateClauseReset()
			{
				_AppColorID_W = null;
				_AppColorName_W = null;
				_AppColorCode_W = null;
				_AppIsActive_W = null;
				_AppDisplayOrder_W = null;
				_AppIsDefault_W = null;
				_AppColorImage_W = null;

				this._entity.Query.FlushAggregateParameters();

			}
	
			private BusinessEntity _entity;
			private TearOffAggregateParameter _tearOff;
			
		}
	
		public AggregateClause Aggregate
		{
			get
			{
				if(_aggregateClause == null)
				{
					_aggregateClause = new AggregateClause(this);
				}
		
				return _aggregateClause;
			}
		}
		
		private AggregateClause _aggregateClause = null;	
		#endregion
	
		protected override IDbCommand GetInsertCommand() 
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblColorInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppColorID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblColorUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblColorDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppColorID);
			p.SourceColumn = ColumnNames.AppColorID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppColorID);
			p.SourceColumn = ColumnNames.AppColorID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppColorName);
			p.SourceColumn = ColumnNames.AppColorName;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppColorCode);
			p.SourceColumn = ColumnNames.AppColorCode;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsActive);
			p.SourceColumn = ColumnNames.AppIsActive;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppDisplayOrder);
			p.SourceColumn = ColumnNames.AppDisplayOrder;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppIsDefault);
			p.SourceColumn = ColumnNames.AppIsDefault;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppColorImage);
			p.SourceColumn = ColumnNames.AppColorImage;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}
