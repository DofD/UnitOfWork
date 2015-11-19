namespace DofD.UofW.DataAccess.Adapters.EF.Test.Impl
{
    using Common.Interface;

    /// <summary>
    ///     ������������ ��������� ���������
    /// </summary>
    public class TestContextConfig : IContextConfig
    {
        /// <summary>
        ///     ���� ���������
        /// </summary>
        public string CacheKey
        {
            get
            {
                return "UofWTest";
            }
        }

        /// <summary>
        ///     ������ �����������
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return "TestUnitOfWork";
            }
        }

        /// <summary>
        ///     ����� �� ���������
        /// </summary>
        public string DefaultSchema
        {
            get
            {
                return "Test";
            }
        }

        /// <summary>
        ///     ���������� SQL
        /// </summary>
        public bool LogSQL
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///     ������������ ���� ��� �������
        /// </summary>
        public string[] NamespaceMaps
        {
            get
            {
                return new[] { "DofD.UofW.Entities" };
            }
        }
    }
}