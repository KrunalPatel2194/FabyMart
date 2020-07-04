
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
	public abstract class _tblReviews : SqlClientEntity
	{
		public _tblReviews()
		{
			this.QuerySource = "tblReviews";
			this.MappingName = "tblReviews";

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
			
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblReviewsLoadAll]", parameters);
		}
	
		//=================================================================
		// public Overridable Function LoadByPrimaryKey()  As Boolean
		//=================================================================
		//  Loads a single row of via the primary key
		//=================================================================
		public virtual bool LoadByPrimaryKey(int AppReviewID)
		{
			ListDictionary parameters = new ListDictionary();
			parameters.Add(Parameters.AppReviewID, AppReviewID);

		
			return base.LoadFromSql("[" + this.SchemaStoredProcedure + "proc_tblReviewsLoadByPrimaryKey]", parameters);
		}
		
		#region Parameters
		protected class Parameters
		{
			
			public static SqlParameter AppReviewID
			{
				get
				{
					return new SqlParameter("@AppReviewID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppReviewDate
			{
				get
				{
					return new SqlParameter("@AppReviewDate", SqlDbType.DateTime, 0);
				}
			}
			
			public static SqlParameter AppReviewStatus
			{
				get
				{
					return new SqlParameter("@AppReviewStatus", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppCustomerID
			{
				get
				{
					return new SqlParameter("@AppCustomerID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppProductDetailID
			{
				get
				{
					return new SqlParameter("@AppProductDetailID", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppRating
			{
				get
				{
					return new SqlParameter("@AppRating", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppComment
			{
				get
				{
					return new SqlParameter("@AppComment", SqlDbType.VarChar, 2147483647);
				}
			}
			
			public static SqlParameter AppPublishedBy
			{
				get
				{
					return new SqlParameter("@AppPublishedBy", SqlDbType.Int, 0);
				}
			}
			
			public static SqlParameter AppPublishedDate
			{
				get
				{
					return new SqlParameter("@AppPublishedDate", SqlDbType.DateTime, 0);
				}
			}
			
			public static SqlParameter AppRemark
			{
				get
				{
					return new SqlParameter("@AppRemark", SqlDbType.VarChar, 2147483647);
				}
			}
			
			public static SqlParameter AppTitle
			{
				get
				{
					return new SqlParameter("@AppTitle", SqlDbType.VarChar, 2147483647);
				}
			}
			
		}
		#endregion		
	
		#region ColumnNames
		public class ColumnNames
		{  
            public const string AppReviewID = "appReviewID";
            public const string AppReviewDate = "appReviewDate";
            public const string AppReviewStatus = "appReviewStatus";
            public const string AppCustomerID = "appCustomerID";
            public const string AppProductDetailID = "appProductDetailID";
            public const string AppRating = "appRating";
            public const string AppComment = "appComment";
            public const string AppPublishedBy = "appPublishedBy";
            public const string AppPublishedDate = "appPublishedDate";
            public const string AppRemark = "appRemark";
            public const string AppTitle = "appTitle";

			static public string ToPropertyName(string columnName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppReviewID] = _tblReviews.PropertyNames.AppReviewID;
					ht[AppReviewDate] = _tblReviews.PropertyNames.AppReviewDate;
					ht[AppReviewStatus] = _tblReviews.PropertyNames.AppReviewStatus;
					ht[AppCustomerID] = _tblReviews.PropertyNames.AppCustomerID;
					ht[AppProductDetailID] = _tblReviews.PropertyNames.AppProductDetailID;
					ht[AppRating] = _tblReviews.PropertyNames.AppRating;
					ht[AppComment] = _tblReviews.PropertyNames.AppComment;
					ht[AppPublishedBy] = _tblReviews.PropertyNames.AppPublishedBy;
					ht[AppPublishedDate] = _tblReviews.PropertyNames.AppPublishedDate;
					ht[AppRemark] = _tblReviews.PropertyNames.AppRemark;
					ht[AppTitle] = _tblReviews.PropertyNames.AppTitle;

				}
				return (string)ht[columnName];
			}

			static private Hashtable ht = null;			 
		}
		#endregion
		
		#region PropertyNames
		public class PropertyNames
		{  
            public const string AppReviewID = "AppReviewID";
            public const string AppReviewDate = "AppReviewDate";
            public const string AppReviewStatus = "AppReviewStatus";
            public const string AppCustomerID = "AppCustomerID";
            public const string AppProductDetailID = "AppProductDetailID";
            public const string AppRating = "AppRating";
            public const string AppComment = "AppComment";
            public const string AppPublishedBy = "AppPublishedBy";
            public const string AppPublishedDate = "AppPublishedDate";
            public const string AppRemark = "AppRemark";
            public const string AppTitle = "AppTitle";

			static public string ToColumnName(string propertyName)
			{
				if(ht == null)
				{
					ht = new Hashtable();
					
					ht[AppReviewID] = _tblReviews.ColumnNames.AppReviewID;
					ht[AppReviewDate] = _tblReviews.ColumnNames.AppReviewDate;
					ht[AppReviewStatus] = _tblReviews.ColumnNames.AppReviewStatus;
					ht[AppCustomerID] = _tblReviews.ColumnNames.AppCustomerID;
					ht[AppProductDetailID] = _tblReviews.ColumnNames.AppProductDetailID;
					ht[AppRating] = _tblReviews.ColumnNames.AppRating;
					ht[AppComment] = _tblReviews.ColumnNames.AppComment;
					ht[AppPublishedBy] = _tblReviews.ColumnNames.AppPublishedBy;
					ht[AppPublishedDate] = _tblReviews.ColumnNames.AppPublishedDate;
					ht[AppRemark] = _tblReviews.ColumnNames.AppRemark;
					ht[AppTitle] = _tblReviews.ColumnNames.AppTitle;

				}
				return (string)ht[propertyName];
			}

			static private Hashtable ht = null;			 
		}			 
		#endregion	

		#region StringPropertyNames
		public class StringPropertyNames
		{  
            public const string AppReviewID = "s_AppReviewID";
            public const string AppReviewDate = "s_AppReviewDate";
            public const string AppReviewStatus = "s_AppReviewStatus";
            public const string AppCustomerID = "s_AppCustomerID";
            public const string AppProductDetailID = "s_AppProductDetailID";
            public const string AppRating = "s_AppRating";
            public const string AppComment = "s_AppComment";
            public const string AppPublishedBy = "s_AppPublishedBy";
            public const string AppPublishedDate = "s_AppPublishedDate";
            public const string AppRemark = "s_AppRemark";
            public const string AppTitle = "s_AppTitle";

		}
		#endregion		
		
		#region Properties
	
		public virtual int AppReviewID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppReviewID);
			}
			set
	        {
				base.Setint(ColumnNames.AppReviewID, value);
			}
		}

		public virtual DateTime AppReviewDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.AppReviewDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.AppReviewDate, value);
			}
		}

		public virtual int AppReviewStatus
	    {
			get
	        {
				return base.Getint(ColumnNames.AppReviewStatus);
			}
			set
	        {
				base.Setint(ColumnNames.AppReviewStatus, value);
			}
		}

		public virtual int AppCustomerID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppCustomerID);
			}
			set
	        {
				base.Setint(ColumnNames.AppCustomerID, value);
			}
		}

		public virtual int AppProductDetailID
	    {
			get
	        {
				return base.Getint(ColumnNames.AppProductDetailID);
			}
			set
	        {
				base.Setint(ColumnNames.AppProductDetailID, value);
			}
		}

		public virtual int AppRating
	    {
			get
	        {
				return base.Getint(ColumnNames.AppRating);
			}
			set
	        {
				base.Setint(ColumnNames.AppRating, value);
			}
		}

		public virtual string AppComment
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppComment);
			}
			set
	        {
				base.Setstring(ColumnNames.AppComment, value);
			}
		}

		public virtual int AppPublishedBy
	    {
			get
	        {
				return base.Getint(ColumnNames.AppPublishedBy);
			}
			set
	        {
				base.Setint(ColumnNames.AppPublishedBy, value);
			}
		}

		public virtual DateTime AppPublishedDate
	    {
			get
	        {
				return base.GetDateTime(ColumnNames.AppPublishedDate);
			}
			set
	        {
				base.SetDateTime(ColumnNames.AppPublishedDate, value);
			}
		}

		public virtual string AppRemark
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppRemark);
			}
			set
	        {
				base.Setstring(ColumnNames.AppRemark, value);
			}
		}

		public virtual string AppTitle
	    {
			get
	        {
				return base.Getstring(ColumnNames.AppTitle);
			}
			set
	        {
				base.Setstring(ColumnNames.AppTitle, value);
			}
		}


		#endregion
		
		#region String Properties
	
		public virtual string s_AppReviewID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppReviewID) ? string.Empty : base.GetintAsString(ColumnNames.AppReviewID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppReviewID);
				else
					this.AppReviewID = base.SetintAsString(ColumnNames.AppReviewID, value);
			}
		}

		public virtual string s_AppReviewDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppReviewDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.AppReviewDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppReviewDate);
				else
					this.AppReviewDate = base.SetDateTimeAsString(ColumnNames.AppReviewDate, value);
			}
		}

		public virtual string s_AppReviewStatus
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppReviewStatus) ? string.Empty : base.GetintAsString(ColumnNames.AppReviewStatus);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppReviewStatus);
				else
					this.AppReviewStatus = base.SetintAsString(ColumnNames.AppReviewStatus, value);
			}
		}

		public virtual string s_AppCustomerID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppCustomerID) ? string.Empty : base.GetintAsString(ColumnNames.AppCustomerID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppCustomerID);
				else
					this.AppCustomerID = base.SetintAsString(ColumnNames.AppCustomerID, value);
			}
		}

		public virtual string s_AppProductDetailID
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppProductDetailID) ? string.Empty : base.GetintAsString(ColumnNames.AppProductDetailID);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppProductDetailID);
				else
					this.AppProductDetailID = base.SetintAsString(ColumnNames.AppProductDetailID, value);
			}
		}

		public virtual string s_AppRating
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppRating) ? string.Empty : base.GetintAsString(ColumnNames.AppRating);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppRating);
				else
					this.AppRating = base.SetintAsString(ColumnNames.AppRating, value);
			}
		}

		public virtual string s_AppComment
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppComment) ? string.Empty : base.GetstringAsString(ColumnNames.AppComment);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppComment);
				else
					this.AppComment = base.SetstringAsString(ColumnNames.AppComment, value);
			}
		}

		public virtual string s_AppPublishedBy
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppPublishedBy) ? string.Empty : base.GetintAsString(ColumnNames.AppPublishedBy);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppPublishedBy);
				else
					this.AppPublishedBy = base.SetintAsString(ColumnNames.AppPublishedBy, value);
			}
		}

		public virtual string s_AppPublishedDate
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppPublishedDate) ? string.Empty : base.GetDateTimeAsString(ColumnNames.AppPublishedDate);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppPublishedDate);
				else
					this.AppPublishedDate = base.SetDateTimeAsString(ColumnNames.AppPublishedDate, value);
			}
		}

		public virtual string s_AppRemark
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppRemark) ? string.Empty : base.GetstringAsString(ColumnNames.AppRemark);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppRemark);
				else
					this.AppRemark = base.SetstringAsString(ColumnNames.AppRemark, value);
			}
		}

		public virtual string s_AppTitle
	    {
			get
	        {
				return this.IsColumnNull(ColumnNames.AppTitle) ? string.Empty : base.GetstringAsString(ColumnNames.AppTitle);
			}
			set
	        {
				if(string.Empty == value)
					this.SetColumnNull(ColumnNames.AppTitle);
				else
					this.AppTitle = base.SetstringAsString(ColumnNames.AppTitle, value);
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
				
				
				public WhereParameter AppReviewID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppReviewID, Parameters.AppReviewID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppReviewDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppReviewDate, Parameters.AppReviewDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppReviewStatus
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppReviewStatus, Parameters.AppReviewStatus);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppCustomerID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppCustomerID, Parameters.AppCustomerID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppProductDetailID
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppProductDetailID, Parameters.AppProductDetailID);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppRating
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppRating, Parameters.AppRating);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppComment
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppComment, Parameters.AppComment);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppPublishedBy
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppPublishedBy, Parameters.AppPublishedBy);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppPublishedDate
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppPublishedDate, Parameters.AppPublishedDate);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppRemark
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppRemark, Parameters.AppRemark);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}

				public WhereParameter AppTitle
				{
					get
					{
							WhereParameter where = new WhereParameter(ColumnNames.AppTitle, Parameters.AppTitle);
							this._clause._entity.Query.AddWhereParameter(where);
							return where;
					}
				}


				private WhereClause _clause;
			}
			#endregion
		
			public WhereParameter AppReviewID
		    {
				get
		        {
					if(_AppReviewID_W == null)
	        	    {
						_AppReviewID_W = TearOff.AppReviewID;
					}
					return _AppReviewID_W;
				}
			}

			public WhereParameter AppReviewDate
		    {
				get
		        {
					if(_AppReviewDate_W == null)
	        	    {
						_AppReviewDate_W = TearOff.AppReviewDate;
					}
					return _AppReviewDate_W;
				}
			}

			public WhereParameter AppReviewStatus
		    {
				get
		        {
					if(_AppReviewStatus_W == null)
	        	    {
						_AppReviewStatus_W = TearOff.AppReviewStatus;
					}
					return _AppReviewStatus_W;
				}
			}

			public WhereParameter AppCustomerID
		    {
				get
		        {
					if(_AppCustomerID_W == null)
	        	    {
						_AppCustomerID_W = TearOff.AppCustomerID;
					}
					return _AppCustomerID_W;
				}
			}

			public WhereParameter AppProductDetailID
		    {
				get
		        {
					if(_AppProductDetailID_W == null)
	        	    {
						_AppProductDetailID_W = TearOff.AppProductDetailID;
					}
					return _AppProductDetailID_W;
				}
			}

			public WhereParameter AppRating
		    {
				get
		        {
					if(_AppRating_W == null)
	        	    {
						_AppRating_W = TearOff.AppRating;
					}
					return _AppRating_W;
				}
			}

			public WhereParameter AppComment
		    {
				get
		        {
					if(_AppComment_W == null)
	        	    {
						_AppComment_W = TearOff.AppComment;
					}
					return _AppComment_W;
				}
			}

			public WhereParameter AppPublishedBy
		    {
				get
		        {
					if(_AppPublishedBy_W == null)
	        	    {
						_AppPublishedBy_W = TearOff.AppPublishedBy;
					}
					return _AppPublishedBy_W;
				}
			}

			public WhereParameter AppPublishedDate
		    {
				get
		        {
					if(_AppPublishedDate_W == null)
	        	    {
						_AppPublishedDate_W = TearOff.AppPublishedDate;
					}
					return _AppPublishedDate_W;
				}
			}

			public WhereParameter AppRemark
		    {
				get
		        {
					if(_AppRemark_W == null)
	        	    {
						_AppRemark_W = TearOff.AppRemark;
					}
					return _AppRemark_W;
				}
			}

			public WhereParameter AppTitle
		    {
				get
		        {
					if(_AppTitle_W == null)
	        	    {
						_AppTitle_W = TearOff.AppTitle;
					}
					return _AppTitle_W;
				}
			}

			private WhereParameter _AppReviewID_W = null;
			private WhereParameter _AppReviewDate_W = null;
			private WhereParameter _AppReviewStatus_W = null;
			private WhereParameter _AppCustomerID_W = null;
			private WhereParameter _AppProductDetailID_W = null;
			private WhereParameter _AppRating_W = null;
			private WhereParameter _AppComment_W = null;
			private WhereParameter _AppPublishedBy_W = null;
			private WhereParameter _AppPublishedDate_W = null;
			private WhereParameter _AppRemark_W = null;
			private WhereParameter _AppTitle_W = null;

			public void WhereClauseReset()
			{
				_AppReviewID_W = null;
				_AppReviewDate_W = null;
				_AppReviewStatus_W = null;
				_AppCustomerID_W = null;
				_AppProductDetailID_W = null;
				_AppRating_W = null;
				_AppComment_W = null;
				_AppPublishedBy_W = null;
				_AppPublishedDate_W = null;
				_AppRemark_W = null;
				_AppTitle_W = null;

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
				
				
				public AggregateParameter AppReviewID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppReviewID, Parameters.AppReviewID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppReviewDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppReviewDate, Parameters.AppReviewDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppReviewStatus
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppReviewStatus, Parameters.AppReviewStatus);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppCustomerID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppCustomerID, Parameters.AppCustomerID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppProductDetailID
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppProductDetailID, Parameters.AppProductDetailID);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppRating
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppRating, Parameters.AppRating);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppComment
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppComment, Parameters.AppComment);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppPublishedBy
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppPublishedBy, Parameters.AppPublishedBy);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppPublishedDate
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppPublishedDate, Parameters.AppPublishedDate);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppRemark
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppRemark, Parameters.AppRemark);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}

				public AggregateParameter AppTitle
				{
					get
					{
							AggregateParameter aggregate = new AggregateParameter(ColumnNames.AppTitle, Parameters.AppTitle);
							this._clause._entity.Query.AddAggregateParameter(aggregate);
							return aggregate;
					}
				}


				private AggregateClause _clause;
			}
			#endregion
		
			public AggregateParameter AppReviewID
		    {
				get
		        {
					if(_AppReviewID_W == null)
	        	    {
						_AppReviewID_W = TearOff.AppReviewID;
					}
					return _AppReviewID_W;
				}
			}

			public AggregateParameter AppReviewDate
		    {
				get
		        {
					if(_AppReviewDate_W == null)
	        	    {
						_AppReviewDate_W = TearOff.AppReviewDate;
					}
					return _AppReviewDate_W;
				}
			}

			public AggregateParameter AppReviewStatus
		    {
				get
		        {
					if(_AppReviewStatus_W == null)
	        	    {
						_AppReviewStatus_W = TearOff.AppReviewStatus;
					}
					return _AppReviewStatus_W;
				}
			}

			public AggregateParameter AppCustomerID
		    {
				get
		        {
					if(_AppCustomerID_W == null)
	        	    {
						_AppCustomerID_W = TearOff.AppCustomerID;
					}
					return _AppCustomerID_W;
				}
			}

			public AggregateParameter AppProductDetailID
		    {
				get
		        {
					if(_AppProductDetailID_W == null)
	        	    {
						_AppProductDetailID_W = TearOff.AppProductDetailID;
					}
					return _AppProductDetailID_W;
				}
			}

			public AggregateParameter AppRating
		    {
				get
		        {
					if(_AppRating_W == null)
	        	    {
						_AppRating_W = TearOff.AppRating;
					}
					return _AppRating_W;
				}
			}

			public AggregateParameter AppComment
		    {
				get
		        {
					if(_AppComment_W == null)
	        	    {
						_AppComment_W = TearOff.AppComment;
					}
					return _AppComment_W;
				}
			}

			public AggregateParameter AppPublishedBy
		    {
				get
		        {
					if(_AppPublishedBy_W == null)
	        	    {
						_AppPublishedBy_W = TearOff.AppPublishedBy;
					}
					return _AppPublishedBy_W;
				}
			}

			public AggregateParameter AppPublishedDate
		    {
				get
		        {
					if(_AppPublishedDate_W == null)
	        	    {
						_AppPublishedDate_W = TearOff.AppPublishedDate;
					}
					return _AppPublishedDate_W;
				}
			}

			public AggregateParameter AppRemark
		    {
				get
		        {
					if(_AppRemark_W == null)
	        	    {
						_AppRemark_W = TearOff.AppRemark;
					}
					return _AppRemark_W;
				}
			}

			public AggregateParameter AppTitle
		    {
				get
		        {
					if(_AppTitle_W == null)
	        	    {
						_AppTitle_W = TearOff.AppTitle;
					}
					return _AppTitle_W;
				}
			}

			private AggregateParameter _AppReviewID_W = null;
			private AggregateParameter _AppReviewDate_W = null;
			private AggregateParameter _AppReviewStatus_W = null;
			private AggregateParameter _AppCustomerID_W = null;
			private AggregateParameter _AppProductDetailID_W = null;
			private AggregateParameter _AppRating_W = null;
			private AggregateParameter _AppComment_W = null;
			private AggregateParameter _AppPublishedBy_W = null;
			private AggregateParameter _AppPublishedDate_W = null;
			private AggregateParameter _AppRemark_W = null;
			private AggregateParameter _AppTitle_W = null;

			public void AggregateClauseReset()
			{
				_AppReviewID_W = null;
				_AppReviewDate_W = null;
				_AppReviewStatus_W = null;
				_AppCustomerID_W = null;
				_AppProductDetailID_W = null;
				_AppRating_W = null;
				_AppComment_W = null;
				_AppPublishedBy_W = null;
				_AppPublishedDate_W = null;
				_AppRemark_W = null;
				_AppTitle_W = null;

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
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblReviewsInsert]";
	
			CreateParameters(cmd);
			
			SqlParameter p;
			p = cmd.Parameters[Parameters.AppReviewID.ParameterName];
			p.Direction = ParameterDirection.Output;
    
			return cmd;
		}
	
		protected override IDbCommand GetUpdateCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblReviewsUpdate]";
	
			CreateParameters(cmd);
			      
			return cmd;
		}
	
		protected override IDbCommand GetDeleteCommand()
		{
		
			SqlCommand cmd = new SqlCommand();
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "[" + this.SchemaStoredProcedure + "proc_tblReviewsDelete]";
	
			SqlParameter p;
			p = cmd.Parameters.Add(Parameters.AppReviewID);
			p.SourceColumn = ColumnNames.AppReviewID;
			p.SourceVersion = DataRowVersion.Current;

  
			return cmd;
		}
		
		private IDbCommand CreateParameters(SqlCommand cmd)
		{
			SqlParameter p;
		
			p = cmd.Parameters.Add(Parameters.AppReviewID);
			p.SourceColumn = ColumnNames.AppReviewID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppReviewDate);
			p.SourceColumn = ColumnNames.AppReviewDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppReviewStatus);
			p.SourceColumn = ColumnNames.AppReviewStatus;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppCustomerID);
			p.SourceColumn = ColumnNames.AppCustomerID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppProductDetailID);
			p.SourceColumn = ColumnNames.AppProductDetailID;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppRating);
			p.SourceColumn = ColumnNames.AppRating;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppComment);
			p.SourceColumn = ColumnNames.AppComment;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppPublishedBy);
			p.SourceColumn = ColumnNames.AppPublishedBy;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppPublishedDate);
			p.SourceColumn = ColumnNames.AppPublishedDate;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppRemark);
			p.SourceColumn = ColumnNames.AppRemark;
			p.SourceVersion = DataRowVersion.Current;

			p = cmd.Parameters.Add(Parameters.AppTitle);
			p.SourceColumn = ColumnNames.AppTitle;
			p.SourceVersion = DataRowVersion.Current;


			return cmd;
		}
	}
}