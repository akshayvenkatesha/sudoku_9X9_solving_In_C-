import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;

public class Sudoku {

	class Number {
		public int Value;
		public boolean IsConstent;

		public Number() {
			Value = 0;
			IsConstent = false;
		}
	}

	public int MaxNumber = 9;
	public int RootMaxNumber = (int) Math.sqrt(MaxNumber);
	private Number[][] _number = new Number[MaxNumber][MaxNumber];

	public static void main(String[] args) throws Exception {
		Sudoku a = new Sudoku();
		a.ReadInputFromFile("e:\\sudoku.txt");
		a.InsertNumbers();
	}

	public void InsertNumbers() {
		int i = 0, j = 0;
		while (i > -1) {
			int n = 1;
			j = 0;
			while (n > 0 && n <= MaxNumber) {
				while (j > -1 && j < MaxNumber) {
					if (n > MaxNumber)
						break;
					if (Place(i, j, n)) {

						_number[i][j].Value = n;
						n++;
						j = 0;
					} else {
						j++;
					}
				}
				if (n == MaxNumber + 1) {
					i++;
					n = 1;
					j = 0;
				} else {
					if (n > 1) {
						n--;
					} else {
						j = GetIndexofN(i, n);
						if (j != -2 && j != -1) {
							_number[i][j].Value = 0;
							j++;
						}
						i--;
						n = MaxNumber;
					}
					while (true) {
						j = GetIndexofN(i, n);
						if (j != -2) {
							_number[i][j].Value = 0;
							j++;
							break;
						} else {
							if (n == 1) {
								j = GetIndexofN(i, n);
								if (j != -2) {
									_number[i][j].Value = 0;
									j++;
								}
								n = MaxNumber;
								i--;
							} else {
								n--;
							}
						}
					}
				}
			}
		}
	}

	private int GetIndexofN(int i, int n) {
		if (i < 0) {
			PrintAll();
			System.out
					.println("Something is Wrong please see the input values are proper");
			System.exit(1);
		}
		for (int j = 0; j < MaxNumber; j++) {

			if (_number[i][j].Value == n && _number[i][j].IsConstent) {
				return -2;
			}
			if (_number[i][j].Value == n && !_number[i][j].IsConstent) {
				return j;
			}
		}
		return -1;
	}

	private boolean Place(int i, int j, int n) {
		if (i >= MaxNumber) {
			PrintAll();
			System.exit(0);
		}
		if (_number[i][j].IsConstent && _number[i][j].Value == n) {
			return true;
		}

		for (int k = 0; k < MaxNumber; k++) {
			if (_number[i][j].Value != 0
					|| (_number[i][k].Value == n || _number[k][j].Value == n)
					|| !Check3X3(i, j, n)) {
				return false;
			}
		}
		return true;
	}

	public void ReadInputFromFile(String filename) throws Exception {
		File file = new File(filename);
		if (!file.exists())
			throw new Exception(filename + " NotFound");
		BufferedReader br = new BufferedReader(new FileReader(file));

		int i = 0;
		String lines;
		while ((lines = br.readLine()) != null) {
			String[] line = lines.split(",");
			for (int j = 0; j < line.length; j++) {
				int value;
				boolean constent = false;
				value = Integer.parseInt(line[j]);
				if (value != 0) {
					constent = true;
				}
				_number[i][j] = new Number();

				_number[i][j].Value = value;
				_number[i][j].IsConstent = constent;
			}
			i++;
		}
		br.close();
	}

	private void PrintAll() {
		String temp = new String();
		for (int k = 0; k < MaxNumber; k++) {
			for (int l = 0; l < MaxNumber; l++) {
				temp += _number[k][l].Value + ",";
			}
			temp += "\n";
		}
		System.out.println(temp);
	}

	private boolean Check3X3(int i, int j, int n) {
		for (int k = (i / RootMaxNumber) * RootMaxNumber; k < ((i + RootMaxNumber) / RootMaxNumber)
				* RootMaxNumber; k++) {
			for (int l = (j / RootMaxNumber) * RootMaxNumber; l < ((j + RootMaxNumber) / RootMaxNumber)
					* RootMaxNumber; l++) {
				if (n == _number[k][l].Value) {
					return false;
				}
			}
		}
		return true;
	}
}
